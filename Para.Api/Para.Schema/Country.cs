using Para.Base.Schema;

namespace Para.Schema;

public class CountryRequest : BaseRequest
{
    public string CountyCode { get; set; } 
    public string Name { get; set; }
}


public class CountryResponse : BaseResponse
{
    public string CountyCode { get; set; } 
    public string Name { get; set; }
}
