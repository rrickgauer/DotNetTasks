using static Tasks.Service.Domain.CliArgs.Cli.Contracts.CommonCliContracts;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.LabelCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.Label;

public class DeleteLabelArgs : ILabelCliIndex, ICliDeleteFlag
{
    public uint? Index { get; set; }
    public bool Force { get; set; } = false;
}
