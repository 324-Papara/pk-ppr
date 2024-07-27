using System.ComponentModel.DataAnnotations.Schema;
using Para.Base.Entity;

namespace Para.Data.Domain;

[Table("Country", Schema = "dbo")]
public class Country : BaseEntity
{
    public string CountyCode { get; set; } 
    public string Name { get; set; }
}
