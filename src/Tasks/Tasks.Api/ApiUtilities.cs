using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Tasks.Auth;
using Tasks.Configurations;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Implementations;
using Tasks.Services.Interfaces;

namespace Tasks.Api;

public static class ApiUtilities
{
    /// <summary>
    /// Setup a new WebApplicationBuilder
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static WebApplicationBuilder GetWebApplicationBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        Configurations.IConfiguration config = new ConfigurationProduction();

        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownProxies.Add(IPAddress.Parse(config.IpAddressVps));
        });

        // setup basic authentication
        builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();

        InjectDependencies(builder);

        return builder;
    }


    /// <summary>
    /// Inject all the dependencies
    /// </summary>
    /// <param name="builder"></param>
    public static void InjectDependencies(WebApplicationBuilder builder)
    {
        // set the appropriate configuration class
        // depends if the app is running in development or production
        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddSingleton<Configurations.IConfiguration, ConfigurationDev>();
        }
        else
        {
            builder.Services.AddSingleton<Configurations.IConfiguration, ConfigurationProduction>();
        }


        builder.Services
        // services
        .AddScoped<IEventServices, EventServices>()
        .AddScoped<IRecurrenceServices, RecurrenceServices>()
        .AddScoped<IEventActionServices, EventActionServices>()
        .AddScoped<IUserServices, UserServices>()
        .AddScoped<IUserEmailVerificationServices, UserEmailVerificationServices>()
        .AddScoped<ILabelServices, LabelServices>()
        .AddScoped<IEventLabelServices, EventLabelServices>()

        // repositories
        .AddScoped<IUserRepository, UserRepository>()
        .AddScoped<IEventRepository, EventRepository>()
        .AddScoped<IRecurrenceRepository, RecurrenceRepository>()
        .AddScoped<IEventActionRepository, EventActionRepository>()
        .AddScoped<IUserEmailVerificationRepository, UserEmailVerificationRepository>()
        .AddScoped<ILabelRepository, LabelRepository>()
        .AddScoped<IEventLabelRepository, EventLabelRepository>()

        // custom filters
        .AddScoped<CustomHeaderFilter>();
    }

    /// <summary>
    /// Configure the web application
    /// </summary>
    /// <param name="webApplication"></param>
    public static void ConfigureWebApplication(WebApplication webApplication)
    {
        webApplication.UseForwardedHeaders();

        //app.UseHttpsRedirection();
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();
        webApplication.MapControllers();
    }
}
