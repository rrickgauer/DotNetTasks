namespace Tasks.Service.Domain.Responses.Basic;

public class BaseResponse
{
    public bool Successful { get; set; } = true;
    public Exception? Exception { get; set; }
    public string? Message { get; set; }

    public bool HasException => Exception != null;
    public bool HasMessage => !string.IsNullOrEmpty(Message);
}

