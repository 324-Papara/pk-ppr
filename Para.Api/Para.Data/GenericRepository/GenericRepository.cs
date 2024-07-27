using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Para.Base.Entity;
using Para.Data.Context;

namespace Para.Data.GenericRepository;

internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ParaDbContext dbContext;

    public GenericRepository(ParaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> GetById(long Id, params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task Insert(TEntity entity)
    {
        entity.IsActive = true;
        entity.InsertDate = DateTime.UtcNow;
        entity.InsertUser = "System";
        await dbContext.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task Delete(long Id)
    {
        var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
        if (entity is not null)
            dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> expression,params string[] includes)
    {
        var query = dbContext.Set<TEntity>().Where(expression).AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.ToListAsync();
    }

    public async Task<List<TEntity>> GetAll(params string[] includes)
    {
        var query = dbContext.Set<TEntity>().AsQueryable();
        query = includes.Aggregate(query, (current, inc) => current.Include(inc));
        return await query.ToListAsync();
    }
}