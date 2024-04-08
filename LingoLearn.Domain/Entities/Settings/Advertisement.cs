namespace Domain.Entities;

public class Advertisement : AggregateRoot
{
    public Advertisement(string name, string description, string imageUrl, bool showInWebsite)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        ShowInWebsite = showInWebsite;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool ShowInWebsite { get; set; }
}