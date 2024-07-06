using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Para.Api;

public class Book
{
    [Required]
    [Range(minimum:1,maximum:10000)]
    [DisplayName("Book id")]
    public int Id { get; set; }
        
        
    [Required]
    [StringLength(maximumLength:50,MinimumLength = 5)]
    [DisplayName("Book name")]
    public string Name { get; set; }
        
        
    [Required]
    [StringLength(maximumLength:50,MinimumLength = 5)]
    [DisplayName("Book author info")]
    public string Author { get; set; }
        
        
    [Required]
    [Range(minimum:50,maximum:400)]
    [DisplayName("Book page count")]
    [PageCount]
    public int PageCount { get; set; }
        
        
    [Required]
    [Range(minimum:1900,maximum:2024)]
    [DisplayName("Book year")]
    public int Year { get; set; }

}