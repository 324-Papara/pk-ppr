using Microsoft.AspNetCore.Http;

namespace Para.Base;

public class SessionContext : ISessionContext
{
    public HttpContext HttpContext { get; set; }
    public Session Session { get; set; }
}