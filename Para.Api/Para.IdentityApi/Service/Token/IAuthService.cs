using Para.Base.Response;
using Para.IdentityApiSchema;

namespace Para.IdentityApi.Service;

public interface IAuthService
{
    Task<ApiResponse<AuthResponse>> Login(AuthRequest request);
    Task<ApiResponse> Logout();
    Task<ApiResponse> ChangePassword(ChangePasswordRequest request);
    Task<ApiResponse> Register(RegisterUserRequest request);
}