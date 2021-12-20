using Registration.Domain.Base;
using Registration.Domain.Entities.Users;

namespace Registration.Domain.Entities.Companies;
public partial class Company : IAggregateRoot
{
    public Company(string name)
    {
        this.Update(name);
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
