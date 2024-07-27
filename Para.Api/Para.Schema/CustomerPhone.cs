using Para.Base.Schema;

namespace Para.Schema;

public class CustomerPhoneRequest : BaseRequest
{
    public long CustomerId { get; set; }
    public string CountyCode { get; set; } 
    public string Phone { get; set; }
    public bool IsDefault { get; set; }    
}


public class CustomerPhoneResponse : BaseResponse
{
    public long CustomerId { get; set; }
    public string CountyCode { get; set; }
    public string Phone { get; set; }
    public bool IsDefault { get; set; }    
    
    public string CustomerName { get; set; }
    public string CustomerIdentityNumber { get; set; }
    public int CustomerNumber { get; set; }
}
