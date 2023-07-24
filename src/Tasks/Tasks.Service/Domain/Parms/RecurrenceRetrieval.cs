using Tasks.Service.Validation;

namespace Tasks.Service.Domain.Parms;


public class RecurrenceRetrieval : IValidDateRange
{
    public Guid UserId { get; set; }
    public DateTime StartsOn { get; set; }
    public DateTime EndsOn { get; set; }
    public List<Guid>? LabelIds { get; set; }
    public GetRecurrencesQueryParms? GetRecurrencesQueryParms { get; set; }
    public bool IsValid() => EndsOn >= StartsOn;


    /// <summary>
    /// Default constructor
    /// </summary>
    public RecurrenceRetrieval() { }


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
        if (GetRecurrencesQueryParms is null || GetRecurrencesQueryParms.Labels is null)
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
