using Registration.Domain.Base;

namespace Registration.Domain.Entities.Users;
public partial class User : IAggregateRoot
{
    public User(string username,
        string password,
        string email)
    {
        this.Update(
            username, 
            password, 
            email);  
    }

    public void Update(string username, 
        string password, 
        string email)
    {
        Username = username;
        Password = password;
        Email = email;
    }

    public void AddCompany(long companyId)
    {
        CompanyId = companyId;
    }

    public void UpdateCompany(long? companyId)
    {
        CompanyId = (long)(companyId != null ? companyId : CompanyId);
    }
}
