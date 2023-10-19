using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;

public class CompleteChecklistItemArgs : IChecklistItemCliChecklistId, IChecklistItemCliIndex
{
    public uint ChecklistCommandLineId { get; set; }

    public uint? ItemCommandLineId { get; set; }
    public bool Incomplete { get; set; } = false;
}


