using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Responses.Basic;

public class LabelServicesResponses
{
    /// <summary>
    /// Response for Get labels request
    /// </summary>
    public class GetLabelsResponse : DataResponse<IEnumerable<Label>>
    {
        public override IEnumerable<Label>? Data { get; set; }
    }

    /// <summary>
    /// Response for Get Label request
    /// </summary>
    public class GetLabelResponse : DataResponse<Label>
    {
        public override Label? Data { get; set; }
    }

    /// <summary>
    /// Response for Put/Post Label request
    /// </summary>
    public class ModifyLabelResponse : DataResponse<Label>
    {
        public override Label? Data { get; set; }
    }


    public class DeleteLabelResponse : DataResponse<int?>
    {
        public override int? Data { get; set; }
    }


}



