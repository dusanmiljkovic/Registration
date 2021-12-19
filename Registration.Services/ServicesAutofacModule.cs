using Autofac;
using Registration.Services.Registration;
using Registration.Services.Registration.Contracts;
using Registration.Services.Users;
using Registration.Services.Users.Contracts;

namespace Registration.Services;
internal class ServicesAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<RegistrationService>().As<IRegistrationService>().InstancePerLifetimeScope();
    }
}
