using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Tasks.Api;
using Tasks.Service.Auth;
using Tasks.Service.Configurations;
using Tasks.Service.Repositories.Implementations;
using Tasks.Service.Repositories.Interfaces;
using Tasks.Service.Services.Implementations;
using Tasks.Service.Services.Interfaces;


var builder = ApiUtilities.GetWebApplicationBuilder(args);

var app = builder.Build();

ApiUtilities.ConfigureWebApplication(app);

app.Run();