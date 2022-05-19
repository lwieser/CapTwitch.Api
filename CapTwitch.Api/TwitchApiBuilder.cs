using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.EntityFrameworkCore;

namespace CapTwitch.Api;

public class TwitchApiBuilder
{
    public static WebApplicationBuilder Builder(string[] strings)
    {
        var webApplicationBuilder = WebApplication.CreateBuilder(strings);

        // Add services to the container.
        webApplicationBuilder.Services.AddControllers();
        webApplicationBuilder.Services.AddScoped(typeof(IService<>),typeof(Service<>));
        webApplicationBuilder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
        webApplicationBuilder.Services.AddScoped<IService<User>, UserService>();
        webApplicationBuilder.Services.AddScoped<IRepository<User>, Repository<User>>();
        webApplicationBuilder.Services.AddScoped<IBadWordChecker, BadWordChecker>();
        webApplicationBuilder.Services.AddDbContextPool<CapTwitchDbContext>(opt =>
        {
            string cs = "Server=localhost;Port=3307;Database=captwitch;Uid=captwitch;Pwd=captwitch;";
            opt.UseMySql(cs, ServerVersion.AutoDetect(cs));
        });
        webApplicationBuilder.Services.AddMvc().AddNewtonsoftJson();
        webApplicationBuilder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "Toto",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        return webApplicationBuilder;
    }

}