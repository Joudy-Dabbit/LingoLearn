using Domain.Enum;

namespace Domain.Entities;

public class Student: User
{
    private Student() { }

    public Student(string fullName, string phoneNumber, string email,
        DateTime? birthDate, Gender gender, string imagUrl, string deviceToken)
    {
        Gender = gender;
        ImagUrl = imagUrl;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        UserName = Guid.NewGuid().ToString();
        Email = email;
        DeviceToken = deviceToken;
    }

    public Gender Gender { get; private set; }
    public string? DeviceToken { get; private set; }
    public string ImagUrl { get; private set; }

    
    private readonly List<Language> _languages = new();
    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();
}