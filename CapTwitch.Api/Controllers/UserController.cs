using CapTwitch.Model.Model;
using CapTwitch.Services;

namespace CapTwitch.Api.Controllers;

public class UserController : GenericController<User>
{
    public UserController(CapTwitchDbContext ctx, IService<User> service) : base(ctx, service)
    {
    }
}