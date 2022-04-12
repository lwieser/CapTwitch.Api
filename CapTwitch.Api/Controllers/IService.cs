using CapTwitch.Api.Model;

namespace CapTwitch.Api.Controllers;

public interface IService<T> where T : class, IStoredObject
{
    T Add(T obj);
}