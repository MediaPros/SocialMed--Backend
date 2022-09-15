using SocialMed.API.Security.Resources;

namespace SocialMed.API.Forums.Resources;

public class ForumResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    
    //relationsship
 
    public int userId { get; set; }
}