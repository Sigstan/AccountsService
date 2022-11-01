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
                        Id = Guid.NewGuid(),
                        Level = AccountLevel.Basic,
                        Status = AccountStatus.Closed
                    },
                    new()
                    {
                        AccountNumber = 1002,
                        Balance = (decimal) 1000.00,
                        Currency = Currency.Eur,
                        Id = Guid.NewGuid(),
                        Level = AccountLevel.Vip,
                        Status = AccountStatus.Open
                    }

                });
            });
        }
    }
}