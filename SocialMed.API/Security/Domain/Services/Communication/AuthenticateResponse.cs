namespace SocialMed.API.Security.Domain.Services.Communication;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string? Image { get; set; }
    public string Email { get; set; }
    public string Specialist { get; set; }
    public int Recommendation { get; set; }
    public string WorkPlace { get; set; }
    public string? Biography { get; set; }
    public string Token { get; set; }
}