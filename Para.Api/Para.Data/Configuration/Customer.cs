using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Para.Data.Domain;

namespace Para.Data.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);

        builder.Property(x => x.FirstName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.LastName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.IdentityNumber).IsRequired(true).HasMaxLength(11);
        builder.Property(x => x.Email).IsRequired(true).HasMaxLength(100);
        builder.Property(x => x.CustomerNumber).IsRequired(true);
        builder.Property(x => x.DateOfBirth).IsRequired(true);

        builder.HasIndex(x => x.IdentityNumber).IsUnique(true);
        builder.HasIndex(x => x.Email).IsUnique(true);
        builder.HasIndex(x => x.CustomerNumber).IsUnique(true);


        builder.HasMany(x => x.CustomerAddresses)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.CustomerPhones)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.CustomerDetail)
            .WithOne(x => x.Customer)
            .HasForeignKey<CustomerDetail>(x => x.CustomerId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}