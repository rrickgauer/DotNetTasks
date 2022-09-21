using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Tasks.Auth;
using Tasks.Configurations;
using Tasks.DependenciesInjector;
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

        ServicesInjector.InjectDependencies(builder.Services, builder.Environment.IsDevelopment());

        return builder;
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
