using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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

public class TokenBuilder
{
    public static readonly string _key = "2LuKn3USiHILQabRjQk9m8ZU1axDoVAe";
    public static readonly string _issuer = "CapTwith";

    public static string BuildToken(string userName, string id)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName ?? throw  new ArgumentNullException(nameof(userName))),
            new Claim(JwtRegisteredClaimNames.Jti, id ?? throw  new ArgumentNullException(nameof(id))),
        };


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // if (roles != null && roles.Count > 0)
        // {
        //     var strFeatures = roles?.SelectMany(x => x.Role.JarvisRoleRightFeatures.Select(y => y.Feature != null ? $"{y.Feature.TechnicalIdentifier}:{y.Feature.Name}:{(int)y.Right}" : null))?.ToList() ?? new List<string>();
        //     strFeatures.RemoveAll(x => x == null);
        //     var strRoles = roles.Select(userRole => $"{userRole.Role.Application}:{userRole.Role.Name}").ToList(); // JRV-112 
        //     strRoles = strRoles.Concat(roles.Where(x => !string.IsNullOrEmpty(x.Role.TechnicalIdentifier)).Select(role => role.Role.TechnicalIdentifier)).ToList();
        //
        //     claims.AddRange(strRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
        //     claims.AddRange(strFeatures.Select(feature => new Claim("feature", feature)));
        // }
        // if (companyIds != null && companyIds.Count > 0)
        // {
        //     claims.AddRange(companyIds.Select(company => new Claim("Companies", company)));
        // }

        var token = new JwtSecurityToken(_issuer,
            _issuer,
            claims,
            expires: DateTime.Now.AddHours(5),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}