namespace Tasks.Service.Domain.Contracts;

public interface ICliPromptSelection<in T> where T : notnull
{
    public static abstract string CliPromptSelectionTitle { get; }
    public static abstract Func<T, string> CliPromptSelectionConverter { get; }
}
