using System.Data.Entity;
using AutoMapper;
using Para.Base.Response;
using Para.IdentityApi.Context;
using Para.IdentityApi.Domain;
using Para.IdentityApi.Schema;

namespace Para.IdentityApi.Service;

public class CategoryService : ICategoryService
{
    private readonly ParaIdentityDbContext dbContext;
    private readonly IMapper mapper;

    public CategoryService(ParaIdentityDbContext dbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task<ApiResponse<CategoryResponse>> Insert(CategoryRequest request)
    {
        var mapped = mapper.Map<Category>(request);
        var entity = await dbContext.Set<Category>().AddAsync(mapped);
        await dbContext.SaveChangesAsync();

        var response = mapper.Map<CategoryResponse>(entity.Entity);
        return new ApiResponse<CategoryResponse>(response);
    }

    public async Task<ApiResponse> Update(long Id, CategoryRequest request)
    {
        var entity = await dbContext.Set<Category>().FirstOrDefaultAsync(x=> x.Id == Id);
        
        entity.UrlName = request.UrlName;
        entity.Name = request.Name;
        await dbContext.SaveChangesAsync();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Delete(long Id)
    {
        var entity = await dbContext.Set<Category>().FirstOrDefaultAsync(x=> x.Id == Id);

        entity.IsActive = false; 
        await dbContext.SaveChangesAsync();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse<List<CategoryResponse>>> GetAll()
    {
        var entityList = await dbContext.Set<Category>().ToListAsync();
        var response = mapper.Map<List<CategoryResponse>>(entityList);
        return new ApiResponse<List<CategoryResponse>>(response);
    }

    public async Task<ApiResponse<CategoryResponse>> GetById(long Id)
    {
        var entity = await dbContext.Set<Category>().FirstOrDefaultAsync(x=> x.Id == Id);
        var response = mapper.Map<CategoryResponse>(entity);
        return new ApiResponse<CategoryResponse>(response);
    }
}