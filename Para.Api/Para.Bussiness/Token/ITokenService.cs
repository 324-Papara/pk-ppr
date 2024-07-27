using Para.Base.Response;
using Para.Data.Domain;

namespace Para.Bussiness.Token;

public interface ITokenService
{
    Task<string> GetToken(User user);
}