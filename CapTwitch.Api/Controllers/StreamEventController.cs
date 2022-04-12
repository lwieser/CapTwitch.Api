using CapTwitch.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace CapTwitch.Api.Controllers;

[Route("[controller]")]
public class StreamEventController : GenericController<StreamEvent>
{
    public StreamEventController(CapTwitchDbContext ctx, IService<StreamEvent> service) : base(ctx, service)
    {
    }
}