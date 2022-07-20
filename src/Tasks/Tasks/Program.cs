using Microsoft.AspNetCore.Authentication;
using Tasks.Configurations;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;
using Tasks.Security;
using Tasks.Services.Implementations;
using Tasks.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// setup basic authentication
builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

ConifigureDependencies(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();
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
        builder.Services.AddSingleton<IConfigs, ConfigurationProduction>();
    }
        

    // services
    builder.Services.AddScoped<IEventServices, EventServices>();
    builder.Services.AddScoped<IRecurrenceServices, RecurrenceServices>();
    builder.Services.AddScoped<IEventActionServices, EventActionServices>();

    // repositories
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IEventRepository, EventRepository>();
    builder.Services.AddScoped<IRecurrenceRepository, RecurrenceRepository>();
    builder.Services.AddScoped<IEventActionRepository, EventActionRepository>();
}
