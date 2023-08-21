namespace Tasks.Service.Repositories.Commands;

public class EventLabelCommands
{
    /// <summary>
    /// @event_id
    /// @label_id
    /// @created_on
    /// </summary>
    public const string Insert = @"
        REPLACE INTO Event_Labels (event_id, label_id, created_on)
        VALUES
            (@event_id, @label_id, @created_on)";



    /// <summary>
    /// Fetch all the assigned labels to a User's event
    /// 
    /// Parms:
    /// @event_id
    /// @user_id
    /// </summary>
    public const string SelectAllByIdAndUserId2 = @"
        SELECT
            l.*
        FROM
            Event_Labels el
            LEFT JOIN Labels l ON l.id = el.label_id
        WHERE
            el.event_id = @event_id
            AND EXISTS (
                SELECT
                    1
                FROM
                    Events e
                WHERE
                    e.id = el.event_id
                    AND e.user_id = @user_id
            )";


    public const string SelectAllByIdAndUserId = @"
        SELECT
            l.*
        FROM
            Event_Labels el
            LEFT JOIN Labels l ON l.id = el.label_id
        WHERE
            el.event_id = @event_id";









    public const string SelectAllByUser = @"
        SELECT
            el.*
        FROM
            Events e
            INNER JOIN Event_Labels el ON el.event_id = e.id
        WHERE
            e.user_id = @user_id";


    public const string BatchInsertTemplate = @"
        REPLACE INTO
            Event_Labels (event_id, label_id)
        SELECT
            @event_id,
            l.id
        FROM
            Labels l
        WHERE
            l.id IN ({0}) 
            AND l.user_id = @user_id";


    public const string DeleteAllByEvent = @"
        DELETE FROM
            Event_Labels el
        WHERE
            el.event_id = @event_id;";


    public const string Delete = @"
        DELETE FROM
            Event_Labels
        WHERE
            event_id = @event_id
            AND label_id = @label_id";

}
