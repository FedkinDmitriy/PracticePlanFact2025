using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderSum = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    OrdersDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.CheckConstraint("OnlyPositiveSumOrder", "\"OrderSum\" >= 0");
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), new DateOnly(2968, 9, 22), "Фродо", "Бэггинс" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), new DateOnly(2931, 3, 1), "Арагорн", "Элесар" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), new DateOnly(10, 1, 1), "Гэндальф", "Серый" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), new DateOnly(2879, 6, 15), "Леголас", "Зелёный Лист" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"), new DateOnly(2879, 4, 5), "Гимли", "сын Глоина" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId", "OrderSum", "OrdersDateTime", "Status" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc1"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), 100.50m, new DateTime(2025, 4, 1, 12, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc2"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), 250.75m, new DateTime(2025, 4, 2, 15, 30, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc3"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), 500m, new DateTime(2025, 4, 3, 10, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc4"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), 75.99m, new DateTime(2025, 4, 3, 18, 45, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc5"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"), 320.10m, new DateTime(2025, 4, 4, 9, 20, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc6"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), 15.30m, new DateTime(2025, 4, 4, 22, 10, 0, 0, DateTimeKind.Utc), 0 },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc7"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"), 780.00m, new DateTime(2025, 4, 5, 14, 5, 0, 0, DateTimeKind.Utc), 2 },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc8"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"), 999.99m, new DateTime(2025, 4, 6, 7, 30, 0, 0, DateTimeKind.Utc), 1 },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc9"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"), 430.75m, new DateTime(2025, 4, 6, 17, 55, 0, 0, DateTimeKind.Utc), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            // Функция: сумма заказов в день рождения клиента
            migrationBuilder.Sql(
            """
            CREATE OR REPLACE FUNCTION get_birthday_orders_total()
            RETURNS TABLE (ClientId UUID, FullName TEXT, birthday DATE, total NUMERIC)
            LANGUAGE SQL
            AS $$
            SELECT
                c."Id" AS ClientId,
                c."FirstName" || ' ' || c."LastName" AS FullName,
                c."DateOfBirth" AS Birthday,
                SUM(o."OrderSum") AS Total
            FROM "Clients" c
            JOIN "Orders" o ON o."ClientId" = c."Id"
            WHERE o."Status" = 2
            AND EXTRACT(MONTH FROM o."OrdersDateTime") = EXTRACT(MONTH FROM c."DateOfBirth")
            AND EXTRACT(DAY FROM o."OrdersDateTime") = EXTRACT(DAY FROM c."DateOfBirth")
            GROUP BY c."Id", c."FirstName", c."LastName", c."DateOfBirth";
            $$;
            """
            );

            // Функция: средний чек по каждому часу суток
            migrationBuilder.Sql(
            """
            CREATE OR REPLACE FUNCTION get_avg_order_sum_per_hour()
            RETURNS TABLE (Hour INTEGER, AvgSum NUMERIC)
            LANGUAGE SQL
            AS $$
            SELECT
                EXTRACT(HOUR FROM o."OrdersDateTime")::INT AS Hour,
                ROUND(AVG(o."OrderSum"), 2) AS AvgSum
            FROM "Orders" o
            WHERE o."Status" = 2
            GROUP BY Hour
            ORDER BY Hour DESC;
            $$;
            """
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.Sql("DROP FUNCTION IF EXISTS get_birthday_orders_total();");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS get_avg_order_sum_per_hour();");
        }
    }
}
