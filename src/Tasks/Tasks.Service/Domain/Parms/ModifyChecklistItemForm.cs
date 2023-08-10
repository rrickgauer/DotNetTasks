using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Domain.Models;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.Parms;

public class ModifyChecklistItemForm : IModelParms<ChecklistItem>
{
    [BindProperty]
    public string? Content { get; set; }

    [BindProperty]
    public bool IsComplete { get; set; } = false;

    [BindProperty]
    public uint Position { get; set; } = 0;

    // IModelParms
    public void CopyFieldsToModel(ChecklistItem model)
    {
        AttributeUtilities.CopyOverProperties(this, model);
    }
}
