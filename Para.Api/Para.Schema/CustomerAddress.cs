using Para.Base.Schema;

namespace Para.Schema;

public class CustomerAddressRequest : BaseRequest
{
    public long CustomerId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string AddressLine { get; set; }
    public string ZipCode { get; set; }
    public bool IsDefault { get; set; }
}

public class CustomerAddressResponse : BaseResponse
{
    public long CustomerId { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string AddressLine { get; set; }
    public string ZipCode { get; set; }
    public bool IsDefault { get; set; }
}