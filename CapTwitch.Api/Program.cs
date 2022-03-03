using CapTwitch.Api.Controllers;
using CapTwitch.Api.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContextPool<CapTwitchDbContext>(opt =>
{
    string cs = "Server=localhost;Port=3307;Database=captwitch;Uid=captwitch;Pwd=captwitch;";
    opt.UseMySql(cs, ServerVersion.AutoDetect(cs));
});
builder.Services.AddMvc().AddNewtonsoftJson();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();
