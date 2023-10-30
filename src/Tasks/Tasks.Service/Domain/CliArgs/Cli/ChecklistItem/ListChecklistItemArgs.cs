using Tasks.Service.Domain.Enums;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;

public class ListChecklistItemArgs : IChecklistItemCliChecklistId
{
    public uint ChecklistCommandLineId { get; set; }

    public bool Interactive { get; set; } = false;
    public CliDataOutputStyle Style { get; set; } = CliDataOutputStyle.Default;
    public CliShowChecklistItemsOption FilterItems { get; set; } = CliShowChecklistItemsOption.All;
}



