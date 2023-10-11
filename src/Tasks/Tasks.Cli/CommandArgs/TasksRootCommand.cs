using System.CommandLine;

namespace Tasks.Cli.CommandArgs;

public class TasksRootCommand : RootCommand
{
    private readonly ChecklistCommandGroup _checklistCommands;

    public TasksRootCommand(ChecklistCommandGroup checklistCommandGroup) : base("Tasks CLI")
    { 
        _checklistCommands = checklistCommandGroup;
    }

    public void SetupCommandGroups()
    {
        _checklistCommands.AddCommandsToRoot(this);
    }
}
