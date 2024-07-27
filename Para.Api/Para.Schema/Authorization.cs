using Para.Base.Schema;

namespace Para.Schema;

public class AuthorizationRequest : BaseRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}


public class AuthorizationResponse : BaseResponse
{
    public DateTime ExpireTime { get; set; }
    public string AccessToken { get; set; }
    public string UserName { get; set; }
}