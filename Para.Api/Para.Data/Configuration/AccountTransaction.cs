using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Para.Data.Domain;

namespace Para.Data.Configuration;

public class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
{
    public void Configure(EntityTypeBuilder<AccountTransaction> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

        builder.Property(x => x.AccountId).IsRequired(true);
        builder.Property(x => x.ReferenceNumber).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DebitAmount).IsRequired().HasPrecision(18,2).HasDefaultValue(0);
        builder.Property(x => x.CreditAmount).IsRequired().HasPrecision(18,2).HasDefaultValue(0);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(50);
        builder.Property(x => x.TransactionDate).IsRequired();
        builder.Property(x => x.TransactionCode).IsRequired().HasMaxLength(10);

        builder.HasIndex(x => x.AccountId);
    }
}