using System.ComponentModel.DataAnnotations.Schema;
using Para.Base.Entity;

namespace Para.Data.Domain;


[Table("CustomerPhone", Schema = "dbo")]
public class CustomerPhone : BaseEntity
{
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    
    public string CountyCode { get; set; } // TUR
    public string Phone { get; set; }
    public bool IsDefault { get; set; }
}
