using CapTwitch.Api.Model;

namespace CapTwitch.Api.Controllers;

public interface IRepository<T> where T : class, IStoredObject
{
    T Add(T obj);
}