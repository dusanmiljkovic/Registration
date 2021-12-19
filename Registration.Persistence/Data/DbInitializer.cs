using Microsoft.EntityFrameworkCore;
using Registration.Domain.Base;
using System.Linq.Expressions;

namespace Registration.Persistence.Data;
public static class DbInitializer
{
    public static void Migrate(RegistrationContext context)
    {
        context.Database.Migrate();
    }

    private static void AddOrUpdate<T>(Expression<Func<T, bool>> predicate, T entity, RegistrationContext context) where T : BaseEntity
    {
        T? savedEntity = Find(predicate, context);
        if (savedEntity is null)
        {
            Add(entity, context);
        }
        else
        {
            context.Entry(savedEntity).State = EntityState.Detached;
            Update(savedEntity.Id, entity, context);
        }
    }

    private static T? Find<T>(Expression<Func<T, bool>> predicate, RegistrationContext context) where T : BaseEntity
    {
        return context.Set<T>().FirstOrDefault(predicate);
    }

    private static T Add<T>(T entity, RegistrationContext context) where T : BaseEntity
    {
        return context.Set<T>().Add(entity).Entity;
    }

    private static T Update<T>(long id, T entity, RegistrationContext context) where T : BaseEntity
    {
        entity.Id = id;
        return context.Update(entity).Entity;
    }
}
