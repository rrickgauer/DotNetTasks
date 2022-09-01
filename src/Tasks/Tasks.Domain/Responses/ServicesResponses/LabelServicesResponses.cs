using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Domain.Responses.ServicesResponses;

public class LabelServicesResponses
{

    public class GetLabelsResponse : BaseResponse<IEnumerable<Label>>
    {
        public override IEnumerable<Label>? Data { get; set; }
    }

    public class GetLabelResponse : BaseResponse<Label>
    {
        public override Label? Data { get; set; }
    }
}



