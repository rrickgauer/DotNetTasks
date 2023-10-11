using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.CliArgs.Cli.Checklist;

public class DeleteChecklistArgs
{
    public bool Force {  get; set; } = false;
    public int? ChecklistReference { get; set; }
}
