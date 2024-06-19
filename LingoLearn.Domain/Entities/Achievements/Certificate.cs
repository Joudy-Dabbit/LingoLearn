namespace Domain.Entities;

public class Certificate : AggregateRoot
{
    public Certificate(string title, string description, string fileUrl, Guid levelId)
    {
        Title = title;
        Description = description;
        FileUrl = fileUrl;
        LevelId = levelId;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public string FileUrl { get; set; }
    
    public Guid LevelId { get; private set; }
    public Level Level { get; private set; }    
    
    public void Modify(string title, string description, string fileUrl)
    {
        Title = title;
        Description = description;
        FileUrl = fileUrl;
    }
}