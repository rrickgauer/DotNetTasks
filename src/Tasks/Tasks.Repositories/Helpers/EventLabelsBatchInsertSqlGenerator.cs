using MySql.Data.MySqlClient;
using Tasks.Domain.Parms;
using Tasks.SQL.Commands;

namespace Tasks.Repositories.Helpers;

public class EventLabelsBatchInsertSqlGenerator
{
    public EventLabelsBatchRequest EventLabelsBatchRequest { get; }
    public Dictionary<string, Guid> NamedParms { get; } = new();
    public string SqlStatement => GetSqlStatement();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="eventLabelsBatchRequest"></param>
    public EventLabelsBatchInsertSqlGenerator(EventLabelsBatchRequest eventLabelsBatchRequest)
    {
        EventLabelsBatchRequest = eventLabelsBatchRequest;

        InitNamedParms();
    }

    /// <summary>
    /// Initialize the named parms dictionary
    /// </summary>
    private void InitNamedParms()
    {
        var labelIds = EventLabelsBatchRequest.LabelIds.ToList();

        for (int i = 0; i < labelIds.Count; i++)
        {
            Guid labelId = labelIds[i];
            string key = $"@label_id_{i}";

            NamedParms.Add(key, labelId);
        }
    }

    /// <summary>
    /// Get the mysql command object 
    /// </summary>
    /// <returns></returns>
    public MySqlCommand GetMySqlCommand()
    {
        MySqlCommand command = new(SqlStatement);
        
        // add event and user ids
        command.Parameters.AddWithValue("@event_id", EventLabelsBatchRequest.EventId);
        command.Parameters.AddWithValue("@user_id", EventLabelsBatchRequest.UserId);

        // add the label id parms
        foreach (var namedParm in NamedParms)
        {
            command.Parameters.AddWithValue(namedParm.Key, namedParm.Value);
        }

        return command;
    }


    #region Build sql statement
    private string GetSqlStatement()
    {
        string template = EventLabelRepositorySql.BatchInsertTemplate;
        string labelIdsList = GetLabelIdsListText();

        string sqlStatement = string.Format(template, labelIdsList);

        return sqlStatement;
    }


    private string GetLabelIdsListText()
    {
        string result = string.Empty;
        bool isFirst = true;

        foreach (var namedParmKey in NamedParms.Keys)
        {
            if (isFirst)
            {
                isFirst = false;
                result += $"{namedParmKey}";
                continue;
            }

            result += $", {namedParmKey}";
        }
        
        //result += "\n";

        return result;
    }

    #endregion

    

}
