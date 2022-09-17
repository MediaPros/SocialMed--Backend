using SocialMed.API.Security.Domain.Models;

namespace SocialMed.API.Security.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> ListAsync();
    Task AddAsync(User user);
    Task<User> FindByIdAsync(int id);

    Task<User> FindByNameAsync(string name);
    Task<User> FindByEmailAsync(string email);
    Task<User> FindBySpecialistAsync(string specialist);

    bool ExistsByEmail(string email);
    User FindById(int id);

    void Update(User user);
    void Remove(User user);
}