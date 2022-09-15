using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Forums.Domain.Models;

public class Rating
{
    public int Id { get; set; }
    public int Rate { get; set; }
    //relationsship
    public int ForumId { get; set; }
    public Forum Forum { get; set; }

}