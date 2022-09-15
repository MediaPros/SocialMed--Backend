namespace SocialMed.API.Medical_Interconsultation.Resources;

public class NotificationResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int UserId { get; set; }
    public string ActionsCodes { get; set; }
    public int ReferencesToUserId { get; set; }
}