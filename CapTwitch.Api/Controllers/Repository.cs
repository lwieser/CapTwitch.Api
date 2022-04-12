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

    public List<T> All()
    {
        return Ctx.Set<T>().ToList();
    }
}