using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.CommonCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;

public class DeleteChecklistItemArgs : IChecklistItemCliChecklistId, IChecklistItemCliIndex, ICliDeleteFlag
{
    public uint ChecklistCommandLineId { get; set; }

    public uint? ItemCommandLineId { get; set; }
    public bool Force { get; set; } = false;
}


