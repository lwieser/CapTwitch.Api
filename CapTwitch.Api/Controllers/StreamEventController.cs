using System.Linq.Expressions;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapTwitch.Api.Controllers;

[Route("[controller]")]
public class StreamEventController : GenericController<StreamEvent>
{
    public StreamEventController(CapTwitchDbContext ctx, IService<StreamEvent> service) : base(ctx, service)
    {
    }

    [AllowAnonymous]
    public override List<StreamEvent> All()
    {
        return base.All();
    }
}