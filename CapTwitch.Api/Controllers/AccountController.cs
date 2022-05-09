using Microsoft.AspNetCore.Mvc;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.AspNetCore.Authorization;

namespace CapTwitch.Api.Controllers;

[Route("[controller]"), AllowAnonymous]
public class AccountController : Controller
{
    private readonly IService<User> _userService;

    public AccountController(IService<User> userService)
    {
        _userService = userService;
    }

    [HttpPost("signin")]
    public ActionResult SignIn([FromBody]SignIn model)
    {
        var user = new User();
        user.Pseudo = model.Pseudo;
        user.PasswordHash =  HasherHelper.Hash(model.Password);
        _userService.Add(user);
        return Ok();
    }

    [HttpPost("logIn")]
    public ActionResult Login([FromBody] SignIn model)
    {
        model.Password = HasherHelper.Hash(model.Password);
        var user = _userService.Get(x => x.PasswordHash == model.Password && x.Pseudo == model.Pseudo);
        if (user != null)
        {
            return Ok(new { token = TokenBuilder.BuildToken(model.Pseudo, model.Pseudo) });
        }

        return Unauthorized();
    }
}