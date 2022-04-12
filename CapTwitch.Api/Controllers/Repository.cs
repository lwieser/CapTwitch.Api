using System.Linq.Expressions;
using CapTwitch.Api.Model;

namespace CapTwitch.Api.Controllers;

public class Repository<T> : IRepository<T> where T : class, IStoredObject
{
    protected CapTwitchDbContext Ctx;

    public Repository(CapTwitchDbContext ctx)
    {
        Ctx = ctx;
    }

    public T Add(T obj)
    {
        Ctx.Set<T>().Add(obj);
        Ctx.SaveChanges();
        return obj;
    }

    public T Get(Expression<Func<T, bool>> condition)
    {
        return Ctx.Set<T>().FirstOrDefault(condition);
    }

    public List<T> All()
    {
        return Ctx.Set<T>().ToList();
    }
}