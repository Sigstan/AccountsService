// <auto-generated />
using System;
using AccountsService.Storage.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountsService.Storage.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.ToTable("Accounts", (string)null);

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

            modelBuilder.Entity("AccountsService.Storage.Entities.CashbackConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("AccountLevel")
                        .HasColumnType("smallint");

                    b.Property<decimal>("CashbackPercentange")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("CashbackConfigurations", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("87bbd860-6098-4f6e-9353-5c911409305c"),
                            AccountLevel = (short)2,
                            CashbackPercentange = 1m
                        },
                        new
                        {
                            Id = new Guid("53bbd840-4032-7f6e-1773-aac91540702a"),
                            AccountLevel = (short)1,
                            CashbackPercentange = 0m
                        });
                });

            modelBuilder.Entity("AccountsService.Storage.Entities.Operation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<short>("Currency")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Operations", (string)null);
                });

            modelBuilder.Entity("AccountsService.Storage.Entities.Operation", b =>
                {
                    b.HasOne("AccountsService.Storage.Entities.Account", "Account")
                        .WithMany("Operations")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("AccountsService.Storage.Entities.Account", b =>
                {
                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
