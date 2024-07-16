namespace Domain.Entities.General;

public class ContactUs : AggregateRoot
{
    public ContactUs(string text, string email,
        string? phoneNumber, string? name)
    {
        Text = text;
        Email = email;
        PhoneNumber = phoneNumber;
        Name = name;
    }

    public string Text { get; set; }

    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Name { get; set; }

    public string? Reply { get; set; }
}