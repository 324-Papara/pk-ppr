using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Para.Data.Domain;

namespace Para.Data.Configuration;

public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        
        builder.Property(x => x.CustomerId).IsRequired(true);
        builder.Property(x => x.Country).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.City).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.AddressLine).IsRequired(true).HasMaxLength(250);
        builder.Property(x => x.ZipCode).IsRequired(false).HasMaxLength(6);
        builder.Property(x => x.IsDefault).IsRequired(true);

    }
}