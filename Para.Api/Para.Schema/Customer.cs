using System.Text.Json.Serialization;
using Para.Base.Schema;

namespace Para.Schema;

public class CustomerRequest  : BaseRequest
{
    [JsonIgnore]
    public int CustomerNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}

public class CustomerResponse : BaseResponse
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public string Email { get; set; }
    public int CustomerNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}