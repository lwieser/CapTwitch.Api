using System.Linq.Expressions;
using CapTwitch.Api.Model;

namespace CapTwitch.Api.Controllers;

public class Service<T> : IService<T> where T : class, IStoredObject
{
    private IRepository<T> repo;

    public Service(IRepository<T> repo)
    {
        this.repo = repo;
    }

    public T Add(T obj)
    {
        return repo.Add(obj);
    }

    public T Get(Expression<Func<T, bool>> condition)
    {
        return repo.Get(condition);
    }
}