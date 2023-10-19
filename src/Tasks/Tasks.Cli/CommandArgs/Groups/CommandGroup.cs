using System.CommandLine;
using System.Reflection;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Utilities;

namespace Tasks.Cli.CommandArgs.Groups;

public abstract class CommandGroup
{
    protected Type CommandGroupType => GetType();
    //public abstract Command TopLevelCommand { get; protected set; }
    public abstract Command TopLevelCommand { get; }
    protected abstract void RegisterHandlers();

    public virtual void AddCommandsToRoot(RootCommand rootCommand)
    {
        RegisterHandlers();
        AddOptionsToCommands();
        AddSubCommands();
        rootCommand.Add(TopLevelCommand);
    }


    protected virtual void AddSubCommands()
    {
        var subCommandProperties = AttributeUtilities.GetPropertiesWithAttribute<SubCommandAttribute>(CommandGroupType);

        foreach (var subCommand in subCommandProperties)
        {
            if (subCommand.GetValue(this) is Command value)
            {
                TopLevelCommand.Add(value);
            }
        }

    }


    protected virtual void AddOptionsToCommands()
    {
        var optionProperties = AttributeUtilities.GetPropertiesWithAttribute<AddOptionAttribute>(CommandGroupType);

        foreach (var property in optionProperties)
        {
            if (property.GetValue(this) is Command value)
            {
                AddCommandOptions(property, value);
            }

        }
    }

    protected virtual void AddCommandOptions(PropertyInfo commandProperty, Command command)
    {
        var optionPropertyNames = AttributeUtilities.GetPropertyAttributes<AddOptionAttribute>(commandProperty.Name, CommandGroupType).Select(a => a.OptionName);

        foreach (var optionName in optionPropertyNames)
        {
            var rawValue = CommandGroupType.GetProperty(optionName)?.GetValue(this, null);

            if (rawValue is Option optionValue)
            {
                command.Add(optionValue);
            }
            else if (rawValue is Argument argumentValue)
            {
                command.Add(argumentValue);
            }
        }
    }

}
