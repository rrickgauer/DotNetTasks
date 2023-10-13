using Spectre.Console;
using Tasks.Service.Domain.Contracts;

namespace Tasks.Service.Services.Interfaces;

public interface IConsoleServices
{

    public Table GetTable<T>(IEnumerable<T> items) where T : ICliTable;
    public Table GetTableWithIndex<T>(IEnumerable<T> items) where T : ICliTable;
}
