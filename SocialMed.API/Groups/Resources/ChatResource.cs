using SocialMed.API.Security.Resources;

namespace SocialMed.API.Groups.Resources;

public class ChatResource
{
    public int Id { get; set; }
    
    //relationships
    public UserResource User { get; set; }
    public UserResource UserDestiny { get; set; }
}