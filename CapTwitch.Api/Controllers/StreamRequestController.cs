using CapTwitch.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace CapTwitch.Api.Controllers;

[Route("[controller]")]
public class StreamRequestController : GenericController<StreamRequest>
{

    public StreamRequestController(CapTwitchDbContext ctx) : base(ctx)
    {
    }
}