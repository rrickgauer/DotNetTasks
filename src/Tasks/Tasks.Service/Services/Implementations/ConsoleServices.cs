using Spectre.Console;
using Spectre.Console.Json;
using Tasks.Service.Domain.CliArgs.Errors;
using Tasks.Service.Domain.Contracts;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;

namespace Tasks.Service.Services.Implementations;

public class ConsoleServices : IConsoleServices
{


    /// <summary>
    /// Get the selected prompt from a console prompt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="selectionPrompt"></param>
    /// <param name="choices"></param>
    /// <returns></returns>
    public T GetSelectedPrompt<T>(SelectionPrompt<T> selectionPrompt, List<T> choices) where T : notnull
    {
        selectionPrompt.AddChoices(choices);

        var selectedItem = AnsiConsole.Prompt(selectionPrompt);

        return selectedItem;
    }

    

    /// <inheritdoc cref="PrintCollection{T}(IEnumerable{T}, CliDataOutputStyle)"/>
    public void PrintCollection<T>(IEnumerable<T> data) where T : ICliTable
    {
        PrintCollection(data, CliDataOutputStyle.Default);
    }
    
    /// <summary>
    /// Print the given collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="style"></param>
    public void PrintCollection<T>(IEnumerable<T> data, CliDataOutputStyle style) where T : ICliTable
    {
        switch(style)
        {
            case CliDataOutputStyle.Json:
                BuildJsonText(data).Print();
                break;

            default:
                BuildTable(data).SetCustomBorderStyle(style).Print();
                break;
        }
    }


    /// <summary>
    /// Get an ansi table
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public Table BuildTable<T>(IEnumerable<T> items) where T : ICliTable
    {
        Table table = new();

        T.AddTableColumns(table);

        foreach (var item in items)
        {
            var row = item.GetTableRow();
            table.AddRow(row.ToArray());
        }

        return table;
    }

    public JsonText BuildJsonText<T>(IEnumerable<T> data) where T : ICliTable
    {
        var dataDict = data.Select(x => x.ToDict());

        var payload = JsonUtilities.ToJsonString(dataDict);

        JsonText jsonText = new(payload)
        {
            MemberStyle = Color.NavajoWhite1,
            BooleanStyle = Color.Blue,
            NullStyle = Color.DarkSlateGray1,
            StringStyle = Color.Green,
            NumberStyle = Color.Orange4,
        };

        return jsonText;
    }

    public void DisplayCommandSuccess()
    {
        Console.WriteLine($"Done");
    }

    public void HandleCliError(CliError cliError)
    {
        HandleCliError(cliError.Message);
    }

    public void HandleCliError(object? message)
    {
        AnsiConsole.Markup($"[red]{message?.ToString()}[/]");
    }


}
