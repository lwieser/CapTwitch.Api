using System.Linq.Expressions;
using CapTwitch.Model.Interfaces;
using CapTwitch.Model.Model;

namespace CapTwitch.Services;

public class Service<T> : IService<T> where T : class, IStoredObject
{
    private readonly IRepository<T> repo;

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

    public List<T> GetAll(Expression<Func<T, bool>> expression)
    {
        return repo.GetAll(expression);
    }

    public T Find(int id)
    {
        return repo.Find(id);
    }
}