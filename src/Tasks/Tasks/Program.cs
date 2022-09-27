using System.Diagnostics;
using Tasks.Configurations;

IConfigs config = new ConfigurationProduction();

ProcessStartInfo startInfo = new();

startInfo.FileName = config.WpfApplicationExe.FullName;

Process.Start(startInfo);

