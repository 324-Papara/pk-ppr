using Para.Base.Response;
using Para.IdentityApi.Schema;

namespace Para.IdentityApi.Service;

public interface ICategoryService
{
    Task<ApiResponse<CategoryResponse>> Insert(CategoryRequest request);
    Task<ApiResponse> Update(long Id, CategoryRequest request);
    Task<ApiResponse> Delete(long Id);
    Task<ApiResponse<List<CategoryResponse>>> GetAll();
    Task<ApiResponse<CategoryResponse>> GetById(long Id);
}