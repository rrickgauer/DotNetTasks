using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Tasks.Configurations;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;
using Tasks.Security;
using Tasks.Services.Implementations;
using Tasks.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownProxies.Add(IPAddress.Parse("104.225.208.163"));
});

// setup basic authentication
builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

ConifigureDependencies(builder);

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


// Setup dependency injection
static void ConifigureDependencies(WebApplicationBuilder builder)
{
    // set the appropriate configuration class
    // depends if the app is running in development or production
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddSingleton<IConfigs, ConfigurationDev>();
    }
    else
    {
        //builder.Services.AddSingleton<IConfigs, ConfigurationProduction>();
        builder.Services.AddSingleton<IConfigs, ConfigurationDev>();
    }

    
    builder.Services
    // services
    .AddScoped<IEventServices, EventServices>()
    .AddScoped<IRecurrenceServices, RecurrenceServices>()
    .AddScoped<IEventActionServices, EventActionServices>()

    // repositories
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IEventRepository, EventRepository>()
    .AddScoped<IRecurrenceRepository, RecurrenceRepository>()
    .AddScoped<IEventActionRepository, EventActionRepository>();
}
