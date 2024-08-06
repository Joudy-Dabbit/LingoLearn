namespace Domain.Entities;

public class Challenge : AggregateRoot
{
    public Challenge(string name, string description,
        Guid languageId, DateTime startDate, DateTime endDate,
        int points, string imageUrl, string coverImageUrl)
    {
        Name = name;
        Description = description;
        LanguageId = languageId;
        StartDate = startDate;
        EndDate = endDate;
        Points = points;
        ImageUrl = imageUrl;
        CoverImageUrl = coverImageUrl;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string CoverImageUrl { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Points { get; set; }
    
    public Guid LanguageId { get; private set; }
    public Language Language { get; private set; }
    
    public  void Modify(string name, string description,
        Guid languageId, DateTime startDate, DateTime endDate,
        int points, string imageUrl, string coverImageUrl)
        {
            Name = name;
            Description = description;
            LanguageId = languageId;
            StartDate = startDate;
            EndDate = endDate;
            Points = points;
            ImageUrl = imageUrl;
            CoverImageUrl = coverImageUrl;
        }
}
