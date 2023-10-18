using Spectre.Console;
using Spectre.Console.Json;
using Spectre.Console.Rendering;
using Tasks.Service.Domain.Contracts;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;

namespace Tasks.Service.Services.Implementations;

public class ConsoleServices : IConsoleServices
{
    /// <summary>
    /// Get an ansi table with order index
    /// </summary>
    /// <typeparam name="TCliTable"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public Table GetTableWithIndex<TCliTable>(IEnumerable<TCliTable> items) where TCliTable : ICliTable
    {
        Table table = new();

        table.AddColumn("Index");
        TCliTable.AddTableColumns(table);

        var itemsList = items.ToList();

        for (int count = 0; count < itemsList.Count; count++)
        {
            var item = itemsList[count];

            var row = item.GetTableRow().Prepend($"{count}").ToArray();

            table.AddRow(row);
        }

        return table;
    }

    /// <summary>
    /// Get an ansi table
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <returns></returns>
    public Table GetTable<T>(IEnumerable<T> items) where T : ICliTable
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


    /// <summary>
    /// Print the json object
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    public void PrintJsonObject<T>(T data)
    {
        var text = JsonUtilities.ToJsonString(data);

        JsonText jsonText = new(text)
        {
            MemberStyle = Color.Green,
        };

        AnsiConsole.Write(new Panel(jsonText));
    }


}
