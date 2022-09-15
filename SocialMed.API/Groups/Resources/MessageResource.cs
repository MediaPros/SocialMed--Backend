using SocialMed.API.Security.Resources;

namespace SocialMed.API.Groups.Resources;

public class MessageResource
{
    public int Id { get; set; }
    
    public UserResource User { get; set; }

    public UserResource UserDestiny { get; set; }
    
    public ChatResource Chat { get; set; }
    
    public string Content { get; set; }
}