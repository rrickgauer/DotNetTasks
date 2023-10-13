using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.Checklist;

public class ViewChecklistArgs : IChecklistCliIndex
{
    public int? Index { get; set; }
}
