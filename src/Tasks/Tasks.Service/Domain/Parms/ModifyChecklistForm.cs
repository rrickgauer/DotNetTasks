using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.Parms;

public class ModifyChecklistForm : IModelParms<Checklist>
{
    [BindProperty]
    public string? Title { get; set; }

    [BindRequired]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ChecklistType Type { get; set; } = ChecklistType.List;


    // IModelParms
    public void CopyFieldsToModel(Checklist model)
    {
        AttributeUtilities.CopyOverProperties(this, model);
    }
}
