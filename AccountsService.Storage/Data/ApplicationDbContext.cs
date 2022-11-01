using AccountsService.Storage.Entities;
using AccountsService.Storage.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AccountsService.Storage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CashbackConfiguration> CashbackConfigurations { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(p => p.Balance)
                    .HasPrecision(18, 2);

                entity.Property(e => e.Currency)
                    .HasConversion(new EnumToNumberConverter<Currency, short>());

                entity.Property(e => e.Level)
                    .HasConversion(new EnumToNumberConverter<AccountLevel, short>());

                entity.Property(e => e.Status)
                    .HasConversion(new EnumToNumberConverter<AccountStatus, short>());

                entity.HasData(new List<Account>()
                {
                    new()
                    {
                        AccountNumber = 1001,
                        Balance = (decimal) 1000.00,
                        Currency = Currency.Eur,
                        Id = new Guid("388e9979-10c7-4fbe-92c7-35083cab0a3f"),
                        Level = AccountLevel.Basic,
                        Status = AccountStatus.Closed
                    },
                    new()
                    {
                        AccountNumber = 1002,
                        Balance = (decimal) 1000.00,
                        Currency = Currency.Eur,
                        Id = new Guid("78991147-6678-4d94-9aa1-1738f9686d5b"),
                        Level = AccountLevel.Vip,
                        Status = AccountStatus.Open
                    }

                });
            });

            modelBuilder.Entity<CashbackConfiguration>(entity =>
            {
                entity.Property(e => e.AccountLevel)
                     .HasConversion(new EnumToNumberConverter<AccountLevel, short>());

                entity.Property(p => p.CashbackPercentange)
                    .HasPrecision(18, 2);

                entity.HasData(new List<CashbackConfiguration>()
                {
                    new()
                    {
                        Id = new Guid("87bbd860-6098-4f6e-9353-5c911409305c"),
                        AccountLevel = AccountLevel.Vip,
                        CashbackPercentange = 1
                    },
                    new()
                    {
                        Id = new Guid("53bbd840-4032-7f6e-1773-aac91540702a"),
                        AccountLevel = AccountLevel.Basic,
                        CashbackPercentange = 0
                    },
                });
            }); 
            
            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(e => e.Type)
                     .HasConversion(new EnumToNumberConverter<OperationType, short>());
                
                entity.Property(e => e.Currency)
                     .HasConversion(new EnumToNumberConverter<Currency, short>());

                entity.Property(p => p.Amount)
                    .HasPrecision(18, 2);

                entity.HasOne(x => x.Account)
                    .WithMany(x => x.Operations)
                    .HasForeignKey(x => x.AccountId)
                    .IsRequired();
            });
        }
    }
}