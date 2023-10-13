using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistCliContracts;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.CommonCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.Checklist;

public class DeleteChecklistArgs : IChecklistCliIndex, ICliDeleteFlag
{
    public bool Force {  get; set; } = false;
    public int? Index { get; set; }
}
