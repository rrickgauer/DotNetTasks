#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tasks.Service.Domain.Models;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.Parms;

/// <summary>
/// Required fields that must be provided in a request to modify a label
/// </summary>
public class UpdateLabelForm : IModelParms<Label>
{
    [BindRequired]
    public string Name { get; set; }
    
    [BindRequired]
    public string Color { get; set; }


    // IModelParms
    public void CopyFieldsToModel(Label model)
    {
        AttributeUtilities.CopyOverProperties(this, model);
    }
}
