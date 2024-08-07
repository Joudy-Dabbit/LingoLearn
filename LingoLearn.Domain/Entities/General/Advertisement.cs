namespace Domain.Entities.General;

public class Advertisement : AggregateRoot
{
    public Advertisement(string title, string description, string imagesUrl, 
        bool showInWebsite, string companyName, double price)
    { 
        Title = title;
        Description = description;
        ImagesUrl = imagesUrl;
        ShowInWebsite = showInWebsite;
        CompanyName = companyName;
        Price = price;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public bool ShowInWebsite { get; set; }
    public string ImagesUrl { get; set; } 
    public string CompanyName { get; set; }
    public double Price { get; set; }
    
    public void Modify(string title, string description, string imagesUrl,
        bool showInWebsite, string companyName, double price)
    { 
        Title = title;
        Description = description;
        ImagesUrl = imagesUrl;
        ShowInWebsite = showInWebsite;
        CompanyName = companyName;
        Price = price;
    }
}