using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Forums.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> ListAsync();
    Task AddAsync(Comment comment);
    Task<Comment> FindByIdAsync(int id);
    Task<IEnumerable<Comment>> ListByUserIdAsync(int id);
    Task<IEnumerable<Comment>> ListByForumIdAsync(int id);

    void Update(Comment comment);
    void Remove(Comment comment);
}