using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.Checklist;

public class CloneChecklistArgs : IChecklistCliIndex, IChecklistCliTitle
{
    public uint? CommandLineId { get; set; }
    public string? Title { get; set; }
}
