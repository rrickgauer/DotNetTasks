using Microsoft.AspNetCore.Authentication;
using Tasks.Configurations;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;
using Tasks.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// setup basic authentication
builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddEndpointsApiExplorer();

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



/*
 * Setup dependency injection
 */
static void ConifigureDependencies(WebApplicationBuilder builder)
{
    // set the appropriate configuration class depending on if the app is running in development or production
    if (builder.Environment.IsDevelopment())
        builder.Services.AddSingleton<IConfigs, ConfigurationDev>();
    else
        builder.Services.AddSingleton<IConfigs, ConfigurationProduction>();

    builder.Services.AddScoped<IUserRepository, UserRepository>();
}
