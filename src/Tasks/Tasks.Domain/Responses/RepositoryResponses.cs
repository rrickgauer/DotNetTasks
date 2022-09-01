using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.Responses;

public class RepositoryResponses
{
    public class SelectAllResponse : BaseResponse<DataTable>
    {
        public override DataTable? Data { get; set; }
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
