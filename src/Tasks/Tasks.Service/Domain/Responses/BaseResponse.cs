namespace Tasks.Service.Domain.Responses;

public abstract class BaseResponse<T> : IBaseResponse<T>
{
    public bool Successful { get; set; }
    public Exception? Exception { get; set; }
    public string? Message { get; set; }
    public abstract T? Data { get; set; }
}



