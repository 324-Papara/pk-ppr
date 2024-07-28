using Para.Base.Schema;

namespace Para.Schema;

public class UserRequest : BaseRequest
{
    public long? CustomerId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


public class UserResponse : BaseResponse
{
    public long? CustomerId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public int Status { get; set; } 
    
    public string CustomerName { get; set; }
    public string CustomerIdentityNumber { get; set; }
    public int CustomerNumber { get; set; }
}