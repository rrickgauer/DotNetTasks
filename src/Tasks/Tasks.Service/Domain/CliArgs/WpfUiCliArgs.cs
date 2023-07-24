using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.CliArgs;

public class WpfUiCliArgs
{
    [Option('d', "debug", Required = false, HelpText = "Run the application in development mode.")]
    public bool Debug { get; set; } = false;
}
