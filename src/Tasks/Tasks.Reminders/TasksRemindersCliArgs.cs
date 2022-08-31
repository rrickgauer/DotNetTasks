
using CommandLine;

namespace Tasks.Reminders;

public class TasksRemindersCliArgs
{
    [Option('d', "development", Required = false, HelpText = "Run in development.")]
    public bool Development { get; set; }

}
