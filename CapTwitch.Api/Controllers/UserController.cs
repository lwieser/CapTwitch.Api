using CapTwitch.Api.Model;

namespace CapTwitch.Api.Controllers;

public class UserController : GenericController<User>
{
    public UserController(CapTwitchDbContext ctx, IService<User> service) : base(ctx, service)
    {
    }
}