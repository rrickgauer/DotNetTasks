using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tasks.Service.Domain.Parms;

/// <summary>
/// Required fields that must be provided in a request to modify a label
/// </summary>
public class UpdateLabelForm
{
    [BindRequired]
    public string Name { get; set; }

    [BindRequired]
    public string Color { get; set; }
}
