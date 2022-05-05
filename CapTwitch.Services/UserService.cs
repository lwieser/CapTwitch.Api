using System.Linq.Expressions;
using CapTwitch.Model.Model;

namespace CapTwitch.Services;

public class UserService : IService<User>
{
    private readonly IRepository<User> repo;
    private readonly IBadWordChecker _badWordChecker;

    public UserService(IRepository<User> repo, IBadWordChecker badWordChecker)
    {
        this.repo = repo;
        _badWordChecker = badWordChecker;
    }

    public User Add(User obj)
    {
        if (obj == null) return null; 
        if (String.IsNullOrWhiteSpace(obj.Pseudo))
        {
            return null;
        }

        if (_badWordChecker.IsBad(obj.Pseudo))
        {
            return null;
        }

        return repo.Add(obj);
    }

    public User Get(Expression<Func<User, bool>> condition)
    {
        return repo.Get(condition);
    }
}