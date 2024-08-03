using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Para.IdentityApi.Domain;

namespace Para.IdentityApi.Context;

public class ParaIdentityDbContext : IdentityDbContext
{
    public ParaIdentityDbContext(DbContextOptions<ParaIdentityDbContext> options) : base(options)
    {
        
    }
    
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryUserConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}