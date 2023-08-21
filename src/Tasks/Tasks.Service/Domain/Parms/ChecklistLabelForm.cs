using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tasks.Service.Domain.Models;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.Parms;

public class ChecklistLabelForm : IModelParms<ChecklistLabel>
{
    [BindRequired]
    public Guid ChecklistId { get; set; }

    [BindRequired]
    public Guid LabelId { get; set; }


    // IModelParms
    public void CopyFieldsToModel(ChecklistLabel model)
    {
        AttributeUtilities.CopyOverProperties(this, model);
    }
}
