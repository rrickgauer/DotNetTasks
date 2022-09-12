﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using Tasks.Validation;

namespace Tasks.Domain.Parms;


public class RecurrenceRetrieval : IValidDateRange
{
    public Guid UserId { get; set; }
    public DateTime StartsOn { get; set; }
    public DateTime EndsOn { get; set; }
    public List<Guid>? LabelIds { get; private set; }
    public GetRecurrencesQueryParms GetRecurrencesQueryParms { get; private set; }
    public bool IsValid() => EndsOn >= StartsOn;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="getRecurrencesQueryParms"></param>
    /// <param name="userId"></param>
    public RecurrenceRetrieval(GetRecurrencesQueryParms getRecurrencesQueryParms, Guid userId) 
    {
        GetRecurrencesQueryParms = getRecurrencesQueryParms;
        StartsOn = GetRecurrencesQueryParms.StartsOn;
        EndsOn = GetRecurrencesQueryParms.EndsOn;
        UserId = userId;
    }

    /// <summary>
    /// Parse the label ids
    /// </summary>
    public void ParseLabels()
    {
        if (GetRecurrencesQueryParms.Labels is null)
        {
            return;
        }

        string text = GetRecurrencesQueryParms.Labels;

        var possibleLabelIds = text.Split(',');

        List<Guid> labelIds = new();

        foreach (var possibleLabel in possibleLabelIds)
        {
            Guid labelId = new(possibleLabel);
            labelIds.Add(labelId);
        }


        LabelIds = labelIds;
    }
}
