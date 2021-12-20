using Registration.Domain.Base;
using Registration.Domain.Entities.Users;

namespace Registration.Domain.Entities.Companies;
public partial class Company : IAggregateRoot
{
    public Company(string name)
    {
        this.Update(name);
    }

    public Company(string name, User user)
    {
        this.Update(name);
        AddUser(user);
    }

    public void Update(string name)
    {
        Name = name;
    }

    public void AddUser(User user)
    {
        Users.Add(user);
    }
}
