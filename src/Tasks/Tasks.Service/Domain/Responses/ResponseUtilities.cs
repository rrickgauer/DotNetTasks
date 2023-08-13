using Tasks.Service.Domain.Responses.Basic;

namespace Tasks.Service.Domain.Responses;

public static class ResponseUtilities
{
    public static void TransferDataResponse<T>(DataResponse<T> source, DataResponse<T> destination)
    {
        TransferDataResponse(source, destination);
        destination.Data = source.Data;
    }


    public static void TransferBaseResponse(BaseResponse source, BaseResponse destination)
    {
        destination.Exception = source.Exception;
        destination.Successful = source.Successful;
        destination.Message = source.Message;
    }
}
