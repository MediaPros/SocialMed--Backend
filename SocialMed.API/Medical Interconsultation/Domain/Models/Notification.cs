using SocialMed.API.Security.Domain.Models;

namespace SocialMed.API.Medical_Interconsultation.Domain.Models;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string ActionsCodes { get; set; }
    public int ReferencesToUserId { get; set; }
}