using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Para.Data.Domain;

namespace Para.Data.Configuration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        
        builder.Property(x => x.CountyCode).IsRequired(true).HasMaxLength(3);
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);

        builder.HasIndex(x => new { x.CountyCode }).IsUnique(true);
    }
}