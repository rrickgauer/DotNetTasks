using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Domain.CliArgs.Cli.Checklist;

public class EditChecklistArgs
{
    public int? ChecklistReference { get; set; }
    public string? Title { get; set; }
}
