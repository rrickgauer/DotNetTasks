using System.Data;

namespace Tasks.Service.Domain.Responses.Basic;

public class RepositoryResponses
{
    public class SelectAllResponse : DataResponse<DataTable>
    {
        public override DataTable? Data { get; set; } = new();
    }

    public class SelectResponse : DataResponse<DataRow>
    {
        public override DataRow? Data { get; set; }
    }

    public class ModifyResponse : DataResponse<int>
    {
        public override int Data { get; set; }
    }
}
