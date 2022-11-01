﻿// <auto-generated />
using System;
using AccountsService.Storage.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountsService.Storage.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221101103949_CreateAccountsTable")]
    partial class CreateAccountsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AccountsService.Storage.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Balance")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<short>("Currency")
                        .HasColumnType("smallint");

                    b.Property<short>("Level")
                        .HasColumnType("smallint");

                    b.Property<short>("Status")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("388e9979-10c7-4fbe-92c7-35083cab0a3f"),
                            AccountNumber = 1001,
                            Balance = 1000m,
                            Currency = (short)1,
                            Level = (short)1,
                            Status = (short)2
                        },
                        new
                        {
                            Id = new Guid("78991147-6678-4d94-9aa1-1738f9686d5b"),
                            AccountNumber = 1002,
                            Balance = 1000m,
                            Currency = (short)1,
                            Level = (short)2,
                            Status = (short)1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}