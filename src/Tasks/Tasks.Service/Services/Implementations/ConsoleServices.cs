using Spectre.Console;
using Tasks.Service.Domain.Contracts;
using Tasks.Service.Services.Interfaces;

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


}
