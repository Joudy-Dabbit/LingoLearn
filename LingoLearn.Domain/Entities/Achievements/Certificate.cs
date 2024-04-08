namespace Domain.Entities;

public class Certificate : AggregateRoot

{
    public Certificate(string title, string description, string fileUrl)
    {
        Title = title;
        Description = description;
        FileUrl = fileUrl;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public string FileUrl { get; set; }
}