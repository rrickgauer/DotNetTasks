using Spectre.Console;
using Spectre.Console.Json;
using Spectre.Console.Rendering;
using Tasks.Service.Domain.Contracts;
using Tasks.Service.Services.Interfaces;
using Tasks.Service.Utilities;

namespace Tasks.Service.Services.Implementations;

public class ConsoleServices : IConsoleServices
{

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
    /// Get the index of the selected choice 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="selectionPrompt"></param>
    /// <param name="choices"></param>
    /// <returns></returns>
    public int GetSelectedPromptIndex<T>(SelectionPrompt<T> selectionPrompt, List<T> choices) where T : notnull
    {
        selectionPrompt.AddChoices(choices);

        var selectedItem = AnsiConsole.Prompt(selectionPrompt);

        return choices.IndexOf(selectedItem);
    }

    

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
