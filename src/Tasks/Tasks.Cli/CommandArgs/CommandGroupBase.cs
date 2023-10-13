using System.CommandLine;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Utilities;

namespace Tasks.Cli.CommandArgs;

public abstract class CommandGroupBase
{
    protected abstract Type CommandGroupType { get; }
    public abstract Command TopLevelCommand { get; protected set; }
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
            var value = subCommand.GetValue(this) as Command;

            if (value != null)
            {
                TopLevelCommand.Add(value);
            }
        }

    }


    protected virtual void AddOptionsToCommands()
    {
        var optionProperties = AttributeUtilities.GetPropertiesWithAttribute<AddToCommandAttribute>(CommandGroupType);

        foreach (var property in optionProperties)
        {
            var value = property.GetValue(this);

            if (value != null)
            {
                AddOptionArgumentToCommands(property.Name, value);
            }

        }
    }

    protected virtual void AddOptionArgumentToCommands(string propertyName, object option)
    {
        var propertyAttributes = AttributeUtilities.GetPropertyAttributes<AddToCommandAttribute, ChecklistCommandGroup>(propertyName);

        var commandNames = propertyAttributes.Select(a => a.CommandName);

        var commandProperties = typeof(ChecklistCommandGroup).GetProperties().Where(p => commandNames.Contains(p.Name));

        foreach (var prop in commandProperties)
        {
            var command = prop.GetValue(this) as Command;

            var basetype = option.GetType().BaseType;

            dynamic optionParsed = option.GetType().BaseType == typeof(Option) ? (Option)option : (Argument)option;
            command?.Add(optionParsed);
        }
    }

}
