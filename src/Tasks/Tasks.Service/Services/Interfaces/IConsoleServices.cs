using Spectre.Console;
using Spectre.Console.Json;
using Spectre.Console.Rendering;
using Tasks.Service.Domain.Contracts;
using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Services.Interfaces;

public interface IConsoleServices
{
    public T GetSelectedPrompt<T>(SelectionPrompt<T> selectionPrompt, List<T> choices) where T : notnull;

    public void PrintCollection<T>(IEnumerable<T> data) where T : ICliTable;
    public void PrintCollection<T>(IEnumerable<T> data, CliDataOutputStyle style) where T : ICliTable;

    public JsonText BuildJsonText<T>(IEnumerable<T> data) where T : ICliTable;
    
    public Table BuildTable<T>(IEnumerable<T> items) where T : ICliTable;

    public void DisplayCommandSuccess();
}
