﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                            DateOfBirth = new DateOnly(2968, 9, 22),
                            FirstName = "Фродо",
                            LastName = "Бэггинс"
                        },
                        new
                        {
                            Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                            DateOfBirth = new DateOnly(2931, 3, 1),
                            FirstName = "Арагорн",
                            LastName = "Элесар"
                        },
                        new
                        {
                            Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
                            DateOfBirth = new DateOnly(10, 1, 1),
                            FirstName = "Гэндальф",
                            LastName = "Серый"
                        },
                        new
                        {
                            Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
                            DateOfBirth = new DateOnly(2879, 6, 15),
                            FirstName = "Леголас",
                            LastName = "Зелёный Лист"
                        },
                        new
                        {
                            Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"),
                            DateOfBirth = new DateOnly(2879, 4, 5),
                            FirstName = "Гимли",
                            LastName = "сын Глоина"
                        });
                });

            modelBuilder.Entity("Data.Models.Functions.AvgOrderSumPerHour", b =>
                {
                    b.Property<decimal>("AvgSum")
                        .HasColumnType("numeric");

                    b.Property<int>("Hour")
                        .HasColumnType("integer");

                    b.ToTable((string)null);

                    b.ToView(null, (string)null);
                });

            modelBuilder.Entity("Data.Models.Functions.BirthdayOrderTotal", b =>
                {
                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.ToTable((string)null);

                    b.ToView(null, (string)null);
                });

            modelBuilder.Entity("Data.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("OrderSum")
                        .HasColumnType("numeric(8,2)");

                    b.Property<DateTime>("OrdersDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders", t =>
                        {
                            t.HasCheckConstraint("OnlyPositiveSumOrder", "\"OrderSum\" >= 0");
                        });

                    b.HasData(
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc1"),
                            ClientId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                            OrderSum = 100.50m,
                            OrdersDateTime = new DateTime(2025, 4, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc2"),
                            ClientId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                            OrderSum = 250.75m,
                            OrdersDateTime = new DateTime(2025, 4, 2, 15, 30, 0, 0, DateTimeKind.Utc),
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc3"),
                            ClientId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
                            OrderSum = 500m,
                            OrdersDateTime = new DateTime(2025, 4, 3, 10, 0, 0, 0, DateTimeKind.Utc),
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc4"),
                            ClientId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
                            OrderSum = 75.99m,
                            OrdersDateTime = new DateTime(2025, 4, 3, 18, 45, 0, 0, DateTimeKind.Utc),
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc5"),
                            ClientId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5"),
                            OrderSum = 320.10m,
                            OrdersDateTime = new DateTime(2025, 4, 4, 9, 20, 0, 0, DateTimeKind.Utc),
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc6"),
                            ClientId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                            OrderSum = 15.30m,
                            OrdersDateTime = new DateTime(2025, 4, 4, 22, 10, 0, 0, DateTimeKind.Utc),
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc7"),
                            ClientId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                            OrderSum = 780.00m,
                            OrdersDateTime = new DateTime(2025, 4, 5, 14, 5, 0, 0, DateTimeKind.Utc),
                            Status = 2
                        },
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc8"),
                            ClientId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
                            OrderSum = 999.99m,
                            OrdersDateTime = new DateTime(2025, 4, 6, 7, 30, 0, 0, DateTimeKind.Utc),
                            Status = 1
                        },
                        new
                        {
                            Id = new Guid("cccccccc-cccc-cccc-cccc-ccccccccccc9"),
                            ClientId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
                            OrderSum = 430.75m,
                            OrdersDateTime = new DateTime(2025, 4, 6, 17, 55, 0, 0, DateTimeKind.Utc),
                            Status = 2
                        });
                });

            modelBuilder.Entity("Data.Models.Order", b =>
                {
                    b.HasOne("Data.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Data.Models.Client", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
