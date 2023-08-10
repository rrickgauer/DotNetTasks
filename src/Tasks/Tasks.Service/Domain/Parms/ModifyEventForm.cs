#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using Tasks.Service.Domain.Enums;
using Tasks.Service.Domain.Models;
using Tasks.Service.Utilities;

namespace Tasks.Service.Domain.Parms;

public class ModifyEventForm : IModelParms<Event>
{
    [BindRequired]
    public string Name { get; set; }

    [BindProperty]
    public string? Description { get; set; }

    [BindProperty]
    public string? PhoneNumber { get; set; }

    [BindProperty]
    public string? Location { get; set; }

    [BindRequired]
    public DateTime StartsOn { get; set; }

    [BindProperty]
    public DateTime? EndsOn { get; set; }

    [BindProperty]
    public TimeSpan? StartsAt { get; set; }

    [BindProperty]
    public TimeSpan? EndsAt { get; set; }

    [BindProperty]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Frequency Frequency { get; set; } = Frequency.ONCE;

    [BindProperty]
    public uint? Separation { get; set; }

    [BindProperty]
    public int? RecurrenceDay { get; set; }

    [BindProperty]
    public int? RecurrenceWeek { get; set; }

    [BindProperty]
    public int? RecurrenceMonth { get; set; }


    // IModelParms
    public void CopyFieldsToModel(Event model)
    {
        AttributeUtilities.CopyOverProperties(this, model);   
    }
}
