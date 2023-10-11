using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Domain.CliArgs.Cli.Checklist;

[Verb("checklist", HelpText = "Checklist commands")]
public class ChecklistCommand
{
    [Value(0, Required = true)]
    public ChecklistCliSubCommand SubCommand { get; set; }




    


}
