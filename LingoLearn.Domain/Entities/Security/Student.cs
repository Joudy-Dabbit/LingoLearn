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
    
    public Student(string fullName, string phoneNumber, string email,
        DateTime? birthDate, Gender gender, string imagUrl)
    {
        Gender = gender;
        ImagUrl = imagUrl;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        UserName = Guid.NewGuid().ToString();
        Email = email;
    }

    public Gender Gender { get; private set; }
    public string? DeviceToken { get; private set; }
    public string ImagUrl { get; private set; }
    public int Score { get; private set; }

    
    private readonly List<StudentLanguage> _selectedLanguages = new();
    public IReadOnlyCollection<StudentLanguage> SelectedLanguages => _selectedLanguages.AsReadOnly();    
    
    
    private readonly List<StudentLesson> _studentLessons = new();
    public IReadOnlyCollection<StudentLesson> StudentLessons => _studentLessons.AsReadOnly();
    
    
    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    
    
    public void Modify(string fullName, DateTime? birthDate, string email, 
        string phoneNumber, Gender gender, string imagUrl)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
        Email = email;
        Gender = gender;
        ImagUrl = imagUrl;
    }
   
    public void SelectLanguage(Guid languageId)
    {
        var studentLanguage = new StudentLanguage(Id, languageId);
        _selectedLanguages.Add(studentLanguage);
    }

    public void AddScore(int score) => Score += score;
}