using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;

public class NewChecklistItemArgs : IChecklistItemCliChecklistId, IChecklistItemCliContent
{
    public uint ChecklistCommandLineId { get; set; }

    public string? Content { get; set; }   
}