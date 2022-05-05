using CapTwitch.Model.Interfaces;
using System.Linq.Expressions;

namespace CapTwitch.Services;

public interface IRepository<T> where T : class, IStoredObject
{
    T Add(T obj);
    T Get(Expression<Func<T, bool>> condition);
}