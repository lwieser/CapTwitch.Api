using System.Text;
using CapTwitch.Api;
using CapTwitch.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = TwitchApiBuilder.Builder(args);
builder.Services.SetupJwt();
var app = builder.Build();

app.UseCors("AllowOrigin");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireAuthorization();
});

app.Run();

