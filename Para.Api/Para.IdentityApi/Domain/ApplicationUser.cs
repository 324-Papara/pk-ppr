using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Para.IdentityApi.Domain;

public class ApplicationUser :  IdentityUser
{
    public string IdentityNo { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
}


public class  ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.IdentityNo).IsRequired().HasMaxLength(15);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(150);
    }
}