using Autofac;
using Registration.Domain.Interfaces;
using Registration.Persistence.Data;
using Registration.Persistence.Repositories.Common;
using System.Reflection;

namespace Registration.Persistence;

/// <summary>
/// The <see cref="Module"/> used to register dependencies in the services layer.
/// </summary>
internal class PersistenceAutofacModule : Autofac.Module
{
    /// <inheritdoc/>
    protected override void Load(ContainerBuilder builder)
    {
        var persistenceAssembly = Assembly.GetExecutingAssembly();
        builder
            .RegisterAssemblyTypes(persistenceAssembly)
            .AsClosedTypesOf(typeof(IRepository<>))
            .InstancePerLifetimeScope();

        builder
            .RegisterGeneric(typeof(BaseRepository<>))
            .As(typeof(IRepository<>))
            .InstancePerLifetimeScope();

        builder.RegisterType<RegistrationContext>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }
}
