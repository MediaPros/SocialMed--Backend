namespace SocialMed.API.Forums.Resources;

public class RatingResource
{
    public int Id { get; set; }
    public int Rate { get; set; }
    //relationsship
    public ForumResource Forum { get; set; }

}