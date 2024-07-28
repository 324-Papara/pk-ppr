using Para.Data.Domain;
using Para.Data.GenericRepository;

namespace Para.Data.UnitOfWork;

public interface IUnitOfWork
{
    Task Complete(); 
    Task CompleteWithTransaction();
    IGenericRepository<Customer> CustomerRepository { get; }
    IGenericRepository<CustomerDetail> CustomerDetailRepository { get; }
    IGenericRepository<CustomerAddress> CustomerAddressRepository { get; }
    IGenericRepository<CustomerPhone> CustomerPhoneRepository { get; }
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<Country> CountryRepository { get; }
    IGenericRepository<Account>AccountRepository { get; }
    IGenericRepository<AccountTransaction> AccountTransactionRepository { get; }
}