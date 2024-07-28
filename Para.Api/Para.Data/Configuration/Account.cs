using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Para.Data.Domain;

namespace Para.Data.Configuration;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        
        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.AccountNumber).IsRequired(true);
        builder.Property(x => x.IBAN).IsRequired().HasMaxLength(34);
        builder.Property(x => x.Balance).IsRequired().HasPrecision(18,2).HasDefaultValue(0);
        builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(3);
        builder.Property(x => x.OpenDate).IsRequired();

        builder.HasMany(x => x.AccountTransactions)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}