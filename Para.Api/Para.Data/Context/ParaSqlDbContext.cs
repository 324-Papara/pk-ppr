using Microsoft.EntityFrameworkCore;
using Para.Data.Configuration;
using Para.Data.Domain;

namespace Para.Data.Context;

public class ParaSqlDbContext : DbContext
{
    public ParaSqlDbContext(DbContextOptions<ParaSqlDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<CustomerPhone> CustomerPhones { get; set; }
    public DbSet<CustomerDetail> CustomerDetails { get; set; }
    
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerDetailConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerAddressConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerPhoneConfiguration());
        
    }
}