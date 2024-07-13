using Para.Data.Context;
using Para.Data.Domain;
using Para.Data.GenericRepository;

namespace Para.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ParaDbContext dbContext;
    
    public IGenericRepository<Customer> CustomerRepository { get; }
    public IGenericRepository<CustomerDetail> CustomerDetailRepository { get; }
    public IGenericRepository<CustomerAddress> CustomerAddressRepository { get; }
    public IGenericRepository<CustomerPhone> CustomerPhoneRepository { get; }
    
    

    public UnitOfWork(ParaDbContext dbContext)
    {
        this.dbContext = dbContext;

        CustomerRepository = new GenericRepository<Customer>(this.dbContext);
        CustomerDetailRepository = new GenericRepository<CustomerDetail>(this.dbContext);
        CustomerAddressRepository = new GenericRepository<CustomerAddress>(this.dbContext);
        CustomerPhoneRepository = new GenericRepository<CustomerPhone>(this.dbContext);
    }

    public void Dispose()
    {
    }

    public async Task Complete()
    {
        await dbContext.SaveChangesAsync();
    }
    
    public async Task CompleteWithTransaction()
    {
        using (var dbTransaction = await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                await dbContext.SaveChangesAsync();
                await dbTransaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}