
namespace SocialMed.API.Security.Resources;

public class UserResource
{
    public int Id { get; set; } 
    public string Name { get; set; } 
    public string LastName { get; set; } 
    public int Age { get; set; } 
    public string Image { get; set; }
    
    public string Email { get; set; }
    public string Specialist { get; set; }
    public int Recommendation { get; set; }
    public string WorkPlace { get; set; }
    public string Password { get; set; }
    public string Biography { get; set; }
}