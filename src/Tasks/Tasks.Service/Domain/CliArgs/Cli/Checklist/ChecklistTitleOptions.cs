using CommandLine;

namespace Tasks.Service.Domain.CliArgs.Cli.Checklist;

public class ChecklistTitleOptions
{
    [Option('t', "title")]
    public string? Title { get; set; } = null;
}
