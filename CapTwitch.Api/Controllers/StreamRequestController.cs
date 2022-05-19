using System.Linq.Expressions;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using LinqKit;
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
    [HttpGet("")]
    public List<StreamRequest> All(bool? toValidateOnly, bool? validatedOnly)
    {
        Expression<Func<StreamRequest, bool>> condition = streamRequest => true;
        if (!HttpContext.User.Identity.IsAuthenticated)
        {
            condition = streamRequest => streamRequest.ValidatedAt.HasValue;
        }

        if (toValidateOnly.HasValue && toValidateOnly.Value)
        {
            condition = condition.And(x => !x.ValidatedAt.HasValue);
        }
        if (validatedOnly.HasValue && validatedOnly.Value)
        {
            condition = condition.And(x => x.ValidatedAt.HasValue);
        }
        return Service.GetAll(condition);
    }

    [NonAction]
    public override List<StreamRequest> All()
    {
        Expression<Func<StreamRequest, bool>> condition = streamRequest => true;
        if (!HttpContext.User.Identity.IsAuthenticated)
        {
            condition = streamRequest => streamRequest.ValidatedAt.HasValue;
        }
        return Service.GetAll(condition);
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