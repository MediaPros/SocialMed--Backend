using SocialMed.API.Security.Resources;

namespace SocialMed.API.Reports.Resources;

public class ReportResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }

    public UserResource User { get; set; }
}