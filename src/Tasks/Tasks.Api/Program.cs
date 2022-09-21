using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using Tasks.Api;
using Tasks.Auth;
using Tasks.Configurations;
using Tasks.Repositories.Implementations;
using Tasks.Repositories.Interfaces;
using Tasks.Services.Implementations;
using Tasks.Services.Interfaces;


var builder = ApiUtilities.GetWebApplicationBuilder(args);

var app = builder.Build();

ApiUtilities.ConfigureWebApplication(app);

app.Run();