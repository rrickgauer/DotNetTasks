namespace Tasks.SQL.Commands;

public sealed class EventActionRepositorySql
{
    public const string Modify = @"
        REPLACE INTO Event_Actions 
            (event_id, on_date, event_action_type_id, created_on)
        VALUES
            (@event_id, @on_date, @event_action_type_id, @created_on)";

    public const string Delete = @"
        DELETE FROM 
            Event_Actions ea
        WHERE 
            ea.event_id                 = @event_id
            AND ea.on_date              = @on_date
            AND ea.event_action_type_id = @event_action_type_id;";

    public const string Select = @"
        SELECT 
            ea.event_id             AS event_id,
            ea.on_date              AS on_date,
            ea.event_action_type_id AS event_action_type_id,
            ea.created_on           AS created_on
        FROM 
            Event_Actions ea
        WHERE 
            ea.event_id                 = @event_id
            AND ea.on_date              = @on_date
            AND ea.event_action_type_id = @event_action_type_id
        LIMIT 
            1";
}
