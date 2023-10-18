using Spectre.Console;
using Spectre.Console.Rendering;
using Tasks.Service.Domain.Contracts;

namespace Tasks.Service.Services.Interfaces;

public interface IConsoleServices
{
    public Table GetTable<T>(IEnumerable<T> items) where T : ICliTable;
    public Table GetTableWithIndex<T>(IEnumerable<T> items) where T : ICliTable;
    public T GetSelectedPrompt<T>(SelectionPrompt<T> selectionPrompt, List<T> choices) where T : notnull;
    public void PrintJsonObject<T>(T data);
}
