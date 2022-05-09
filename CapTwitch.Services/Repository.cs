using System.Linq.Expressions;
using CapTwitch.Model.Interfaces;
using CapTwitch.Model.Model;

namespace CapTwitch.Services;

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

    public List<T> GetAll(Expression<Func<T, bool>> expression)
    {
        var dbSet = Ctx.Set<T>().AsQueryable();
        if (expression != null)
        {
            dbSet = dbSet.Where(expression);
        }

        return dbSet.ToList();
    }

    public T Find(int id)
    {
        return Ctx.Set<T>().Find(id);
    }

    public List<T> All()
    {
        return Ctx.Set<T>().ToList();
    }
}