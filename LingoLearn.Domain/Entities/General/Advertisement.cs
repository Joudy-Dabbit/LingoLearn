namespace Domain.Entities.General;

public class Advertisement : AggregateRoot
{
    public Advertisement(string title, string description, string imageUrl, bool showInWebsite)
    { 
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        ShowInWebsite = showInWebsite;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool ShowInWebsite { get; set; }
    
    public void Modify(string title, string description, string imageUrl, bool showInWebsite)
    { 
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        ShowInWebsite = showInWebsite;
    }
}