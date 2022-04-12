using CapTwitch.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CapTwitch.Api.Controllers;

[Route("[controller]")]
public class StreamRequestController : GenericController<StreamRequest>
{
    public StreamRequestController(CapTwitchDbContext ctx, IService<StreamRequest> service) : base(ctx, service)
    {
    }

    [AllowAnonymous]
    public override List<StreamRequest> All()
    {
        return base.All();
    }

    [AllowAnonymous]
    public override StreamRequest Post(StreamRequest se)
    {
        return base.Post(se);
    }


    [HttpPost("upvote")]
    public ActionResult UpVote()
    {
        return Ok();
    }
    [HttpPost("downvote")]
    public ActionResult DownVote()
    {
        return Ok();
    }
}