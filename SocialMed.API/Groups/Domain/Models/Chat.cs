using SocialMed.API.Security.Domain.Models;

namespace SocialMed.API.Groups.Domain.Models;

public class Chat
{ 
    public int Id { get; set; }
    
    //relationships
    public int UserId { get; set; }
    public User User { get; set; }
    public int UserDestinyId { get; set; }
    public User UserDestiny { get; set; }
    public IList<Message> Messages { get; set; } = new List<Message>();
}