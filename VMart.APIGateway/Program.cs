using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json");
//builder.Services.AddAuthentication("Bearer")
//        .AddJwtBearer("Bearer", options =>
//        {
//            options.Authority = "https://localhost:7039"; // Replace with your Identity Server URL
//            options.RequireHttpsMetadata = false; // Set to true in production
//            options.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateAudience = false
//            };
//        });
builder.Services.AddOcelot();

var app = builder.Build();
//app.UseAuthentication();
//app.UseAuthorization();
app.UseOcelot().Wait();
app.MapGet("/", () => "Hello World!");

app.Run();
