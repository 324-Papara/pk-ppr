using AutoMapper;
using MediatR;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Token;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.Command;

public class AuthorizationCommandHandler : IRequestHandler<CreateAuthorizationTokenCommand, ApiResponse<AuthorizationResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private ITokenService tokenService;

    public AuthorizationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,ITokenService tokenService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.tokenService = tokenService;
    }

    public async Task<ApiResponse<AuthorizationResponse>> Handle(CreateAuthorizationTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.FirstOrDefault(x => x.UserName == request.Request.UserName,"Customer");
        if (user is null)
            return new ApiResponse<AuthorizationResponse>("Invalid user informations. Check your username or password. E1");

        if (user.Password != CreateMD5(request.Request.Password))
        {
            return new ApiResponse<AuthorizationResponse>("Invalid user informations. Check your username or password. E1");
        }

        if (user.Status != 1 )
            return new ApiResponse<AuthorizationResponse>("Invalid user informations. Check your username or password. E2");

        var token = await tokenService.GetToken(user);
        AuthorizationResponse response = new AuthorizationResponse()
        {
            ExpireTime = DateTime.Now.AddMinutes(5),
            AccessToken = token,
            UserName = user.UserName
        };

        return new ApiResponse<AuthorizationResponse>(response);
    }
    
    private string CreateMD5(string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes).ToLower();

        }
    }
}