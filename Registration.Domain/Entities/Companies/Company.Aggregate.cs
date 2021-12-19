using Registration.Domain.Base;

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
}
