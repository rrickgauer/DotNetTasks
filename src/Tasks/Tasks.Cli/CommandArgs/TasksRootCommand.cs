using System.CommandLine;

namespace Tasks.Cli.CommandArgs;

public class TasksRootCommand : RootCommand
{
    private readonly ChecklistCommandGroup _checklistCommands;
    private readonly LabelCommandGroup _labelCommands;

    public TasksRootCommand(ChecklistCommandGroup checklistCommandGroup, LabelCommandGroup labelCommandGroup) : base("Tasks CLI")
    { 
        _checklistCommands = checklistCommandGroup;
        _labelCommands = labelCommandGroup;
    }

    public void SetupCommandGroups()
    {
        _checklistCommands.AddCommandsToRoot(this);
        _labelCommands.AddCommandsToRoot(this);
    }
}
