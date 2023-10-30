using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Tasks.Service.Configurations;
using Tasks.Service.Repositories.Implementations;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.DependenciesInjector;
using Tasks.Service.Auth;
using Tasks.Service.Converters;
using Tasks.Api.Other;

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

        ConfigureControllers(builder);
        ConfigureIpAddress(builder);

        builder.Services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();

        InjectDependencies(builder);

        return builder;
    }

    private static void InjectDependencies(WebApplicationBuilder builder)
    {
        bool isDevelopment = builder.Environment.IsDevelopment();
        
        ApiServicesInjector servicesInjector = new(isDevelopment, builder.Services);
        servicesInjector.BuildServices();
    }


    private static void ConfigureControllers(WebApplicationBuilder builder)
    {
        builder.Services
        .AddControllers(options =>
        {
            options.Filters.Add<HttpResponseExceptionFilter>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
        });
    }

    private static void ConfigureIpAddress(WebApplicationBuilder builder)
    {
        IConfigs config = new ConfigurationProduction();

        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownProxies.Add(IPAddress.Parse(config.IpAddressVps));
        });
    }





    /// <summary>
    /// Configure the web application
    /// </summary>
    /// <param name="webApplication"></param>
    public static void BuildWebApplication(WebApplication webApplication)
    {
        webApplication.UseForwardedHeaders();

        //app.UseHttpsRedirection();
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();
        webApplication.MapControllers();
    }
}
