using Para.Base.Schema;

namespace Para.Schema;

public class AccountTransactionRequest : BaseRequest
{
    public long AccountId { get; set; }
    public string ReferenceNumber { get; set; }   
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionCode { get; set; }
}


public class AccountTransactionResponse : BaseResponse
{
    public long AccountId { get; set; }
    public string ReferenceNumber { get; set; }   
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionCode { get; set; }
    
    public string CustomerName { get; set; }
    public string CustomerIdentityNumber { get; set; }
    public int CustomerNumber { get; set; }
}