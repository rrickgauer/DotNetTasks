using Microsoft.AspNetCore.Mvc.ModelBinding;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Tasks.Service.Domain.Parms;

/// <summary>
/// Parms required for the PUT: /user request
/// </summary>
public class UpdateUserRequestForm
{
    [BindRequired]
    public string Email { get; set; }

    [BindRequired]
    public bool DeliverReminders { get; set; }
}
