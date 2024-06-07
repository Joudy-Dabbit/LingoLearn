namespace Domain.Entities;

public class Admin : User
{
    private Admin() { }

    public Admin(string fullName, string email)
    {
        FullName = fullName;
        UserName = Guid.NewGuid().ToString();
        Email = email;
    }
}