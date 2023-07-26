namespace Tasks.Service.Domain.Responses.Basic;

public abstract class DataResponse<T> : IDataResponse<T>
{
    public bool Successful { get; set; }
    public Exception? Exception { get; set; }
    public string? Message { get; set; }
    public abstract T? Data { get; set; }
}

