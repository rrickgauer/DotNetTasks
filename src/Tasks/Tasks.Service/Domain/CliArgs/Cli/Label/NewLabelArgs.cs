using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.LabelCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.Label;

public class NewLabelArgs : ILabelCliName, ILabelCliColor
{
    public string? Name { get; set; }
    public string? Color { get; set; }
}
