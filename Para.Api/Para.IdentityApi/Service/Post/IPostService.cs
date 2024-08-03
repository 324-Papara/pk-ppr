using Para.Base.Response;
using Para.IdentityApi.Schema;

namespace Para.IdentityApi.Service;

public interface IPostService
{
    Task<ApiResponse<PostResponse>> Insert(PostRequest request);
    Task<ApiResponse> Update(long Id, PostRequest request);
    Task<ApiResponse> Delete(long Id);
    Task<ApiResponse<List<PostResponse>>> GetAll();
    Task<ApiResponse<PostResponse>> GetById(long Id);
}