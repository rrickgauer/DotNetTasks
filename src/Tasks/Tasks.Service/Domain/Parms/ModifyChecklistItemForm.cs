using Microsoft.AspNetCore.Mvc;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Parms;

public class ModifyChecklistItemForm : IModelParms<ChecklistItem>
{
    [BindProperty]
    public string? Content { get; set; }

    [BindProperty]
    public DateTime? CompletedOn { get; set; } = null;

    [BindProperty]
    public uint Position { get; set; } = 0;

    // IModelParms
    public void CopyFieldsToModel(ChecklistItem model)
    {
        model.Content = Content;
        model.CompletedOn = CompletedOn;
        model.Position = Position;
    }
}
