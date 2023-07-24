using System.Data;

namespace Tasks.Service.Domain.Responses;

public class RepositoryResponses
{
    public class SelectAllResponse : BaseResponse<DataTable>
    {
        public override DataTable? Data { get; set; } = new();
    }

    public class SelectResponse : BaseResponse<DataRow>
    {
        public override DataRow? Data { get; set; }
    }

    public class ModifyResponse : BaseResponse<int>
    {
        public override int Data { get; set; }
    }
}
