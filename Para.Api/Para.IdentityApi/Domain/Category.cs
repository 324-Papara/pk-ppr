using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Para.Base.Entity;

namespace Para.IdentityApi.Domain;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string UrlName { get; set; } 
    
    public virtual List<Post> Posts { get; set; }
}

public class CategoryUserConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        builder.Property(x => x.UrlName).IsRequired().HasMaxLength(150);
        
        builder.HasMany(x => x.Posts)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);
    }
}