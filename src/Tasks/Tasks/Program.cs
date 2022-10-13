using System.Diagnostics;
using Tasks.Configurations;
using Tasks.Domain.CliArgs;
using Tasks.Utilities;

// parse the cli args
TasksCliArgs tasksCliArgs = CliArgUtilities.ParseArgs<TasksCliArgs>(args) ?? new();

// setup the cli args that we are going to pass to the wpf application
WpfUiCliArgs guiArgs = new()
{
    Debug = tasksCliArgs.Debug,
};

// determine the appropriate config
IConfigs config = tasksCliArgs.Debug ? new ConfigurationDev() : new ConfigurationProduction();

// setup the process info
ProcessStartInfo startInfo = new()
{
    FileName = config.WpfApplicationExe.FullName,
    Arguments = CliArgUtilities.ToArgs(guiArgs),
};

// fire off the gui application process
Process.Start(startInfo);

