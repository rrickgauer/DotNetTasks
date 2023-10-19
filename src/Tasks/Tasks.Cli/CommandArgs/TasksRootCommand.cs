using System.CommandLine;
using Tasks.Cli.CommandArgs.Groups;

namespace Tasks.Cli.CommandArgs;

public class TasksRootCommand : RootCommand
{
    private readonly ChecklistCommandGroup _checklistCommands;
    private readonly LabelCommandGroup _labelCommands;
    private readonly ChecklistItemCommandGroup _checklistItemCommands;

    public TasksRootCommand(ChecklistCommandGroup checklistCommandGroup, LabelCommandGroup labelCommandGroup, ChecklistItemCommandGroup checklistItemCommands) : base("Tasks CLI")
    {
        _checklistCommands = checklistCommandGroup;
        _labelCommands = labelCommandGroup;
        _checklistItemCommands = checklistItemCommands;
    }

    public void SetupCommandGroups()
    {
        _checklistCommands.AddCommandsToRoot(this);
        _checklistItemCommands.AddCommandsToRoot(this);
        _labelCommands.AddCommandsToRoot(this);
    }
}
