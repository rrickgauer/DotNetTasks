using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tasks.Service.Domain.Models;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.Parms;

public class EventLabelForm : IModelParms<EventLabel>
{
    [BindRequired]
    public Guid EventId { get; set; }

    [BindRequired]
    public Guid LabelId { get; set; }


    // IModelParms
    public void CopyFieldsToModel(EventLabel model)
    {
        AttributeUtilities.CopyOverProperties(this, model);
    }
}
