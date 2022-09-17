using SocialMed.API.Security.Domain.Models;

namespace SocialMed.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    string GenerateToken(User user);
    int? ValidateToken(string token);
}