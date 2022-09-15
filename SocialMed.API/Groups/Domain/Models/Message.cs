using SocialMed.API.Security.Domain.Models;

namespace SocialMed.API.Groups.Domain.Models;

public class Message
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    public int UserDestinyId { get; set; }
    public User UserDestiny { get; set; }
    public string Content { get; set; }
    
    public int ChatId { get; set; }
    public Chat Chat { get; set; }
}