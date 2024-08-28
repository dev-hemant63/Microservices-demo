using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json");
//builder.Services.AddAuthentication("Bearer")
//        .AddJwtBearer("Bearer", options =>
//        {
//            options.Authority = "https://your-identity-server"; // Your Identity Server URL
//            options.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateAudience = false
//            };
//        });
builder.Services.AddOcelot();

var app = builder.Build();

app.UseOcelot().Wait();
app.MapGet("/", () => "Hello World!");

app.Run();
