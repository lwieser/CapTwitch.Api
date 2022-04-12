using CapTwitch.Api.Controllers;

var builder = TwitchApiBuilder.Builder(args);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.Run();