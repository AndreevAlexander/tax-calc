﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxCalculator.Data;

#nullable disable

namespace TaxCalculator.Data.Migrations
{
    [DbContext(typeof(TaxContext))]
    [Migration("20220503094926_AlterIncomeTable")]
    partial class AlterIncomeTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TaxCalculator.Domain.Entities.AdditionalSpend", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("AppliedBeforeTax")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TaxProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TaxProfileId");

                    b.ToTable("AdditionalSpends");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("ExchangeRate")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.Income", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("IncomeDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TaxProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TaxProfileId");

                    b.ToTable("Incomes");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.Tax", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<decimal?>("AppliesBefore")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPercentage")
                        .HasColumnType("bit");

                    b.Property<Guid>("TaxProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TaxType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TaxProfileId");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.TaxProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ProfileCurrencyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProfileCurrencyId");

                    b.ToTable("TaxProfiles");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.AdditionalSpend", b =>
                {
                    b.HasOne("TaxCalculator.Domain.Entities.TaxProfile", "TaxProfile")
                        .WithMany("AdditionalSpends")
                        .HasForeignKey("TaxProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaxProfile");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.Income", b =>
                {
                    b.HasOne("TaxCalculator.Domain.Entities.TaxProfile", "TaxProfile")
                        .WithMany("Incomes")
                        .HasForeignKey("TaxProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaxProfile");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.Tax", b =>
                {
                    b.HasOne("TaxCalculator.Domain.Entities.TaxProfile", "TaxProfile")
                        .WithMany("Taxes")
                        .HasForeignKey("TaxProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaxProfile");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.TaxProfile", b =>
                {
                    b.HasOne("TaxCalculator.Domain.Entities.Currency", "ProfileCurrency")
                        .WithMany("TaxProfiles")
                        .HasForeignKey("ProfileCurrencyId");

                    b.Navigation("ProfileCurrency");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.Currency", b =>
                {
                    b.Navigation("TaxProfiles");
                });

            modelBuilder.Entity("TaxCalculator.Domain.Entities.TaxProfile", b =>
                {
                    b.Navigation("AdditionalSpends");

                    b.Navigation("Incomes");

                    b.Navigation("Taxes");
                });
#pragma warning restore 612, 618
        }
    }
}
