namespace Tasks.Service.Domain.Responses.Basic;

public class DataResponse<T> : BaseResponse
{ 
    public T? Data { get; set; }

    public DataResponse() { }

    public DataResponse(T? data)
    {
        Data = data;
    }
}

