using Para.Data.Domain;
using Para.Data.GenericRepository;

namespace Para.Data.UnitOfWork;

public interface IUnitOfWork
{
    Task Complete();
    
    IGenericRepository<Customer> CustomerRepository { get; }
    IGenericRepository<CustomerDetail> CustomerDetailRepository { get; }
    IGenericRepository<CustomerAddress> CustomerAddressRepository { get; }
    IGenericRepository<CustomerPhone> CustomerPhoneRepository { get; }
}