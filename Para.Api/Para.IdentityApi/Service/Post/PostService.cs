using System.Data.Entity;
using AutoMapper;
using Para.Base.Response;
using Para.IdentityApi.Context;
using Para.IdentityApi.Domain;
using Para.IdentityApi.Schema;

namespace Para.IdentityApi.Service;

public class PostService : IPostService
{
    private readonly ParaIdentityDbContext dbContext;
    private readonly IMapper mapper;

    public PostService(ParaIdentityDbContext dbContext, IMapper mapper)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task<ApiResponse<PostResponse>> Insert(PostRequest request)
    {
        var mapped = mapper.Map<Post>(request);
        var entity = await dbContext.Set<Post>().AddAsync(mapped);
        await dbContext.SaveChangesAsync();

        var response = mapper.Map<PostResponse>(entity.Entity);
        return new ApiResponse<PostResponse>(response);
    }

    public async Task<ApiResponse> Update(long Id, PostRequest request)
    {
        var entity = await dbContext.Set<Post>().FirstOrDefaultAsync(x=> x.Id == Id);
        
        entity.UrlName = request.UrlName;
        entity.Content = request.Content;
        entity.Title = request.Title;
        entity.ImageUrl = request.ImageUrl;
        entity.CategoryId = request.CategoryId;
        await dbContext.SaveChangesAsync();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse> Delete(long Id)
    {
        var entity = await dbContext.Set<Post>().FirstOrDefaultAsync(x=> x.Id == Id);

        entity.IsActive = false; 
        await dbContext.SaveChangesAsync();
        
        return new ApiResponse();
    }

    public async Task<ApiResponse<List<PostResponse>>> GetAll()
    {
        var entityList = await dbContext.Set<Post>().Include(x=> x.Category).ToListAsync();
        var response = mapper.Map<List<PostResponse>>(entityList);
        return new ApiResponse<List<PostResponse>>(response);
    }

    public async Task<ApiResponse<PostResponse>> GetById(long Id)
    {
        var entity = await dbContext.Set<Post>().Include(x=> x.Category).FirstOrDefaultAsync(x=> x.Id == Id);
        var response = mapper.Map<PostResponse>(entity);
        return new ApiResponse<PostResponse>(response);
    }
}