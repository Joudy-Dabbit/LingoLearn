using Domain.Entities.Base;
using Domain.Entities.Languages;
using EasyRefreshToken.Abstractions;

namespace Domain.Entities.Security;

public class User : BaseIdentity, IUser<Guid>
{
    protected User(string fullName, string imageUrl, DateTime? birthDate)
    {
        FullName = fullName;
        ImageUrl = imageUrl;
        BirthDate = birthDate;
        UserName = Guid.NewGuid().ToString();
    }

    public string ImageUrl { get; set; }  
    public string FullName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    public DateTimeOffset? DateBlocked { get; set; }
    
    private readonly List<Language> _languages = new();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();
}