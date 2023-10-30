using Spectre.Console;
using Spectre.Console.Json;
using Spectre.Console.Rendering;
using Tasks.Service.Domain.Contracts;
using Tasks.Service.Domain.Enums;

namespace Tasks.Service.Utilities;

public static class AnsiTableExtensions
{
    public static Table SetCustomBorderStyle(this Table table, CliDataOutputStyle style)
    {
        table.Border =  style switch
        {
            CliDataOutputStyle.Default   => TableBorder.Simple,
            CliDataOutputStyle.None     => TableBorder.None,
            CliDataOutputStyle.Markdown => TableBorder.Markdown,
            _                           => throw new NotImplementedException(),
        };

        return table;
    }


    public static IRenderable Print(this IRenderable data)
    {
        AnsiConsole.Write(data);
        return data;
    }

    public static SelectionPrompt<T> BuildSelectionPrompt<T>() where T : ICliPromptSelection<T>
    {
        return new()
        {
            Converter = T.CliPromptSelectionConverter,
            Title = T.CliPromptSelectionTitle,
        };
    }
}
