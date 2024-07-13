namespace Para.Data.GenericRepository;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task Save();
    Task<TEntity?> GetById(long Id);
    Task Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task Delete(long Id);
    Task<List<TEntity>> GetAll();
}