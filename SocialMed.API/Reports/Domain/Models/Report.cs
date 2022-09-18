using SocialMed.API.Security.Domain.Models;

namespace SocialMed.API.Reports.Domain.Models;

public class Report
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }

    //relationsship
    public int UserId { get; set; }
    public User User { get; set; } 

}