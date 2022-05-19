using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CapTwitch.Api.Controllers;

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

        var token = new JwtSecurityToken(_issuer,
            _issuer,
            claims,
            expires: DateTime.Now.AddHours(5),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}