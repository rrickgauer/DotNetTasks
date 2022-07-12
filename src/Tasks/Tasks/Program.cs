using Tasks.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// set the appropriate configuration class depending on if the app is running in development or production
if (builder.Environment.IsDevelopment())
    builder.Services.AddSingleton<IConfigs, ConfigurationDev>();
else
    builder.Services.AddSingleton<IConfigs, ConfigurationProduction>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
