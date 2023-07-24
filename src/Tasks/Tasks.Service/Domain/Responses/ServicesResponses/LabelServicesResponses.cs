using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Responses.ServicesResponses;

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


    public class DeleteLabelResponse : BaseResponse<int?>
    {
        public override int? Data { get; set; }
    }


}



