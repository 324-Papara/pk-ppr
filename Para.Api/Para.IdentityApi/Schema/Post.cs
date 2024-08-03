using Para.Base.Schema;

namespace Para.IdentityApi.Schema;

public class PostRequest : BaseRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string UrlName { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
}


public class PostResponse: BaseResponse
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string UrlName { get; set; }
    public string ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryUrlName { get; set; }
}