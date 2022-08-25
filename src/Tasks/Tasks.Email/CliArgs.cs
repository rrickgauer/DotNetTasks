using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace Tasks.Email
{
    public class CliArgs
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('n', "name", Required = false, HelpText = "The name of the email")]
        public string? Name { get; set; }
    }
}
