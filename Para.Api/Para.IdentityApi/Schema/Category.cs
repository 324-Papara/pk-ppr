using Para.Base.Schema;

namespace Para.IdentityApi.Schema;

public class CategoryRequest : BaseRequest
{
    public string Name { get; set; }
    public string UrlName { get; set; } 
}


public class CategoryResponse : BaseResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string UrlName { get; set; } 
}
