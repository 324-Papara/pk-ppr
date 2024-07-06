using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Para.Data.Domain;

namespace Para.Data.Configuration;

public class CustomerDetailConfiguration : IEntityTypeConfiguration<CustomerDetail>
{
    public void Configure(EntityTypeBuilder<CustomerDetail> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        
        builder.Property(x => x.FatherName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.MotherName).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.MontlyIncome).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.Occupation).IsRequired(true).HasMaxLength(50);
        builder.Property(x => x.EducationStatus).IsRequired(true).HasMaxLength(50);
    }
}