using System.ComponentModel.DataAnnotations.Schema;
using Para.Base.Entity;

namespace Para.Data.Domain;

[Table("CustomerAddress", Schema = "dbo")]
public class CustomerAddress : BaseEntity
{
    public long CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    
    
    public string Country { get; set; }
    public string City { get; set; }
    public string AddressLine { get; set; }
    public string ZipCode { get; set; }
    public bool IsDefault { get; set; }
}