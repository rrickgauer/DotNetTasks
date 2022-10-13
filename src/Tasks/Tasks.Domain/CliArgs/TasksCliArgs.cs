using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.CliArgs;

public class TasksCliArgs
{
    [Option('d', "debug", Required = false, HelpText = "Run the application in debug mode.")]
    public bool Debug { get; set; } = false;
}
