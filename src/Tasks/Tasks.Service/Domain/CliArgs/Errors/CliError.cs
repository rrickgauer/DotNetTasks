namespace Tasks.Service.Domain.CliArgs.Errors;

public class CliError 
{
    public string Message { get; }

    public CliError(string message)
    {
        Message = message;
    }

    public override string ToString()
    {
        return Message;
    }
}
