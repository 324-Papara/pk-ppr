using Para.Base.Schema;

namespace Para.Schema;

public class CustomerDetailRequest : BaseRequest
{
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string EducationStatus { get; set; }
    public string MontlyIncome { get; set; }
    public string Occupation { get; set; }
}

public class CustomerDetailResponse : BaseResponse
{
    public long CustomerId { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string EducationStatus { get; set; }
    public string MontlyIncome { get; set; }
    public string Occupation { get; set; }
}