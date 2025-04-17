﻿// <auto-generated />
using System;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MatheusR.Motok.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MatheusR.Motok.Domain.Entities.Delivery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birth_date");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("cnpj");

                    b.Property<string>("LicenceNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("licence_number");

                    b.Property<string>("LicenceType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("licence_type");

                    b.Property<string>("LicenteImagePath")
                        .HasColumnType("text")
                        .HasColumnName("licence_image_path");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.HasIndex("LicenceNumber")
                        .IsUnique();

                    b.ToTable("deliveries", (string)null);
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.Entities.Motorcycle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("identifier");

                    b.Property<string>("LicencePlate")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("licence_plate");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("model");

                    b.Property<int>("Year")
                        .HasColumnType("integer")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.ToTable("motorcycles", (string)null);
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.Entities.Rent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DeliveryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpectedFinalDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expected_final_date");

                    b.Property<decimal>("ExpectedPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("expected_price");

                    b.Property<DateTime?>("FinalDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("final_date");

                    b.Property<decimal?>("FinalPrice")
                        .HasColumnType("numeric")
                        .HasColumnName("final_price");

                    b.Property<bool?>("HasFine")
                        .HasColumnType("boolean")
                        .HasColumnName("has_fine");

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("initial_date");

                    b.Property<bool>("IsRentActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_rent_active");

                    b.Property<Guid>("MotorcycleId")
                        .HasColumnType("uuid");

                    b.Property<string>("RentPlan")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("rent_plan");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("MotorcycleId");

                    b.ToTable("rents", (string)null);
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.OtherTables.Motorcycle2024", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("MotorcycleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MotorcycleId");

                    b.ToTable("motorcycles_2024", (string)null);
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.Entities.Rent", b =>
                {
                    b.HasOne("MatheusR.Motok.Domain.Entities.Delivery", "Delivery")
                        .WithMany("Rents")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MatheusR.Motok.Domain.Entities.Motorcycle", "Motorcycle")
                        .WithMany("Rents")
                        .HasForeignKey("MotorcycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Delivery");

                    b.Navigation("Motorcycle");
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.Entities.User", b =>
                {
                    b.HasOne("MatheusR.Motok.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.OtherTables.Motorcycle2024", b =>
                {
                    b.HasOne("MatheusR.Motok.Domain.Entities.Motorcycle", "Motorcycle")
                        .WithMany()
                        .HasForeignKey("MotorcycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Motorcycle");
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.Entities.Delivery", b =>
                {
                    b.Navigation("Rents");
                });

            modelBuilder.Entity("MatheusR.Motok.Domain.Entities.Motorcycle", b =>
                {
                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}
