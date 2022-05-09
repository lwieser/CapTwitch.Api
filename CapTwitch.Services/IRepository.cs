using CapTwitch.Model.Interfaces;
using System.Linq.Expressions;
using CapTwitch.Model.Model;

namespace CapTwitch.Services;

public interface IRepository<T> where T : class, IStoredObject
{
    T Add(T obj);
    T Get(Expression<Func<T, bool>> condition);
    List<T> GetAll(Expression<Func<T, bool>> expression = null);
    T Find(int id);
}