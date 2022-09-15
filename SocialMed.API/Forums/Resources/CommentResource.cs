using SocialMed.API.Security.Resources;

namespace SocialMed.API.Forums.Resources;

public class CommentResource
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Content { get; set; }

    public int userId { get; set; }
    public ForumResource Forum { get; set; }
}