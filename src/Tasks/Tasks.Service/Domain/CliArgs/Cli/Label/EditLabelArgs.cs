using static Tasks.Service.Domain.CliArgs.Cli.Contracts.LabelCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.Label;

public class EditLabelArgs : ILabelCliName, ILabelCliColor, ILabelCliIndex
{
    public string? Name { get; set; }
    public string? Color { get; set; }
    public int? Index { get; set; }
}
