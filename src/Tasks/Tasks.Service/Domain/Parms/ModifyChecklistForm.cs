using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;

namespace Tasks.Service.Domain.Parms;

public class ModifyChecklistForm : IModelParms<Checklist>
{
    [BindProperty]
    //[FromForm]
    public string? Title { get; set; }

    [BindRequired]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ChecklistType Type { get; set; } = ChecklistType.List;


    // IModelParms
    public void CopyFieldsToModel(Checklist model)
    {
        model.Title = Title;
        model.ListType = Type;
    }
}
