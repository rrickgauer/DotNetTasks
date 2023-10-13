using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.Checklist;

public class NewChecklistArgs : IChecklistCliTitle
{
    public string? Title { get; set; }
}
