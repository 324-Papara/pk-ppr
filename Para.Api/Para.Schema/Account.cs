using Para.Base.Schema;

namespace Para.Schema;

public class AccountRequest : BaseRequest
{
    public long CustomerId { get; set; }
    public string Name { get; set; }
    public string CurrencyCode { get; set; }
}


public class AccountResponse : BaseResponse
{
    public long CustomerId { get; set; }
    public string Name { get; set; }
    public int AccountNumber { get; set; }
    public string IBAN { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime OpenDate { get; set; }
    
    public string CustomerName { get; set; }
    public string CustomerIdentityNumber { get; set; }
    public int CustomerNumber { get; set; }
}