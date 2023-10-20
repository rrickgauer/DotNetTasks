using System.CommandLine;
using Tasks.Service.Domain.Enums;

namespace Tasks.Cli.CommandArgs.Helpers;

public class SharedComponents
{
    public const string TitleNew = "new";
    public const string TitleEdit = "edit";
    public const string TitleDelete = "delete";

    public static Command GetNewCommand(string desciption)
    {
        return new(TitleNew, desciption);
    }

    public static Command GetEditCommand(string desciption)
    {
        return new(TitleEdit, desciption);
    }

    public static Command GetDeleteCommand(string desciption)
    {
        return new(TitleDelete, desciption);
    }

    public static Option<bool> GetForceOption(string description = "Do not prompt for deletion", bool defaultValue = false)
    {
        return new("--force", getDefaultValue: () => defaultValue, description);
    }

    public static Option<CliDataOutputStyle> GetTableOutputStyleOption(string? description=null, CliDataOutputStyle defaultValue=CliDataOutputStyle.Default)
    {
        description ??= "Data output style";

        return new("--style", () => defaultValue, description);
    }

    public static Option<bool> NoPromptsOption()
    {
        return new("--no-prompts", () => false, "No prompts");
    }
}
