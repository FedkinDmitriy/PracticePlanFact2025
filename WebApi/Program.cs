using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Data;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDBContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IFunctionRepository, FunctionRepository>();

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Practice API",
        Version = "v1",
        Description = "Документация для API проекта PracticePlanFact2025"
    });

    // кастомный фильтр для enum
    c.SchemaFilter<EnumDescriptionSchemaFilter>();

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Practice API V1");
        c.RoutePrefix = string.Empty; // Сделать Swagger доступным по корню: https://localhost:порт/
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


public class EnumDescriptionSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var enumType = context.Type;
            var names = Enum.GetNames(enumType);

            // Добавляем описание для enum
            schema.Description = string.Join(", ",
                names.Select(name =>
                    $"{(int)Enum.Parse(enumType, name)} - {name}"));

            // Оставляем числовые значения
            schema.Enum = Enum.GetValues(enumType)
                .Cast<int>()
                .Select(value => (IOpenApiAny)new OpenApiInteger(value))
                .ToList();
        }
    }
}