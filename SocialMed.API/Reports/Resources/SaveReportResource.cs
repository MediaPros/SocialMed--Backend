using System.ComponentModel.DataAnnotations;

namespace SocialMed.API.Reports.Resources;

public class SaveReportResource
{
    [Required]
    [MaxLength(50)]
    public string Title { get; set; }
    
    [Required]
    public string Content { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    //relationship
    
    [Required]
    public int UserId { get; set; }
}