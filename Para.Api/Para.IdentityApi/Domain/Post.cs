using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Para.Base.Entity;

namespace Para.IdentityApi.Domain;

public class Post : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string UrlName { get; set; }
    public string ImageUrl { get; set; }
    
    public virtual Category Category { get; set; }
    public virtual long CategoryId { get; set; }
}



public class  PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(x => x.InsertDate).IsRequired(true);
        builder.Property(x => x.IsActive).IsRequired(true);
        builder.Property(x => x.InsertUser).IsRequired(true).HasMaxLength(50);
        
        builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.UrlName).IsRequired().HasMaxLength(500);
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(500);
        builder.Property(x => x.CategoryId).IsRequired();
    }
}