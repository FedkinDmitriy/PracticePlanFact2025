using Data.Models.Functions;
using Data.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Data.Models.Enums;

namespace Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<BirthdayOrderTotal> BirthdayOrderTotals { get; set; }
        public DbSet<AvgOrderSumPerHour> AvgOrderSumPerHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().Property(c=>c.FirstName).HasMaxLength(30);
            modelBuilder.Entity<Client>().Property(c=>c.LastName).HasMaxLength(50);
            modelBuilder.Entity<Order>().ToTable(t => t.HasCheckConstraint("OnlyPositiveSumOrder", "\"OrderSum\" >= 0"));
            modelBuilder.Entity<Order>().Property(o => o.OrderSum).HasColumnType("numeric(8,2)");
            modelBuilder.Entity<BirthdayOrderTotal>().HasNoKey();
            modelBuilder.Entity<BirthdayOrderTotal>().ToView(null);
            modelBuilder.Entity<AvgOrderSumPerHour>().HasNoKey();
            modelBuilder.Entity<AvgOrderSumPerHour>().ToView(null);

            modelBuilder.Entity<Order>().HasOne(o => o.Client).WithMany(c => c.Orders).HasForeignKey(o => o.ClientId);


            // фиксированные GUID для клиентов
            var client1Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1");
            var client2Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2");
            var client3Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3");
            var client4Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4");
            var client5Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5");

            modelBuilder.Entity<Client>().HasData(
            new Client { Id = client1Id, FirstName = "Фродо", LastName = "Бэггинс", DateOfBirth = new DateOnly(2968, 9, 22) },
            new Client { Id = client2Id, FirstName = "Арагорн", LastName = "Элесар", DateOfBirth = new DateOnly(2931, 3, 1) },
            new Client { Id = client3Id, FirstName = "Гэндальф", LastName = "Серый", DateOfBirth = new DateOnly(10, 1, 1) },
            new Client { Id = client4Id, FirstName = "Леголас", LastName = "Зелёный Лист", DateOfBirth = new DateOnly(2879, 6, 15) },
            new Client { Id = client5Id, FirstName = "Гимли", LastName = "сын Глоина", DateOfBirth = new DateOnly(2879, 4, 5) });

            // фиксированные GUID для заказов
            var order1Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc1");
            var order2Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc2");
            var order3Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc3");
            var order4Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc4");
            var order5Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc5");
            var order6Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc6");
            var order7Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc7");
            var order8Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc8");
            var order9Id = Guid.Parse("cccccccc-cccc-cccc-cccc-ccccccccccc9");

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = order1Id,
                    ClientId = client1Id,
                    OrderSum = 100.50m,
                    OrdersDateTime = DateTime.SpecifyKind(new DateTime(2025, 4, 1, 12, 0, 0), DateTimeKind.Utc),
                    Status = OrderStatus.Выполнен
                },
                new Order
                {
                    Id = order2Id,
                    ClientId = client2Id,
                    OrderSum = 250.75m,
                    OrdersDateTime = DateTime.SpecifyKind(new DateTime(2025, 4, 2, 15, 30, 0), DateTimeKind.Utc),
                    Status = OrderStatus.Не_обработан
                },
                new Order
                {
                    Id = order3Id,
                    ClientId = client3Id,
                    OrderSum = 500m,
                    OrdersDateTime = DateTime.SpecifyKind(new DateTime(2025, 4, 3, 10, 0, 0), DateTimeKind.Utc),
                    Status = OrderStatus.Выполнен
                },
                new Order
                {
                    Id = order4Id,
                    ClientId = client4Id,
                    OrderSum = 75.99m,
                    OrdersDateTime = DateTime.SpecifyKind(new DateTime(2025, 4, 3, 18, 45, 0), DateTimeKind.Utc),
                    Status = OrderStatus.Не_обработан
                },
                new Order
                {
                    Id = order5Id,
                    ClientId = client5Id,
                    OrderSum = 320.10m,
                    OrdersDateTime = DateTime.SpecifyKind(new DateTime(2025, 4, 4, 9, 20, 0), DateTimeKind.Utc),
                    Status = OrderStatus.Выполнен
                },
                new Order
                {
                    Id = order6Id,
                    ClientId = client1Id,
                    OrderSum = 15.30m,
                    OrdersDateTime = DateTime.SpecifyKind(new DateTime(2025, 4, 4, 22, 10, 0), DateTimeKind.Utc),
                    Status = OrderStatus.Не_обработан
                },
                new Order
                {
                    Id = order7Id,
                    ClientId = client2Id,
                    OrderSum = 780.00m,
                    OrdersDateTime = DateTime.SpecifyKind(new DateTime(2025, 4, 5, 14, 5, 0), DateTimeKind.Utc),
                    Status = OrderStatus.Выполнен
                },
                new Order
                {
                    Id = order8Id,
                    ClientId = client3Id,
                    OrderSum = 999.99m,
                    OrdersDateTime = DateTime.SpecifyKind(new DateTime(2025, 4, 6, 7, 30, 0), DateTimeKind.Utc),
                    Status = OrderStatus.Отменен
                },
                new Order
                {
                    Id = order9Id,
                    ClientId = client4Id,
                    OrderSum = 430.75m,
                    OrdersDateTime = DateTime.SpecifyKind(new DateTime(2025, 4, 6, 17, 55, 0), DateTimeKind.Utc),
                    Status = OrderStatus.Выполнен
                }
            );
        }
    }
}
