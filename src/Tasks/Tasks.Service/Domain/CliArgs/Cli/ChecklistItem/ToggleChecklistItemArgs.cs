using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;

public class ToggleChecklistItemArgs : IChecklistItemCliChecklistId, IChecklistItemCliIndex
{
    public uint ChecklistCommandLineId { get; set; }

    public uint? ItemCommandLineId { get; set; }

}


