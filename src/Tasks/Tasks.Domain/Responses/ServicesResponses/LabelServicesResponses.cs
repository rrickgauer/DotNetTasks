using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Domain.Responses.ServicesResponses;

public class LabelServicesResponses
{
    /// <summary>
    /// Response for Get labels request
    /// </summary>
    public class GetLabelsResponse : BaseResponse<IEnumerable<Label>>
    {
        public override IEnumerable<Label>? Data { get; set; }
    }

    /// <summary>
    /// Response for Get Label request
    /// </summary>
    public class GetLabelResponse : BaseResponse<Label>
    {
        public override Label? Data { get; set; }
    }

    /// <summary>
    /// Response for Put/Post Label request
    /// </summary>
    public class ModifyLabelResponse : BaseResponse<Label>
    {
        public override Label? Data { get; set; }
    }
}



