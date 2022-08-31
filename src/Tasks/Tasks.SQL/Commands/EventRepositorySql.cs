
namespace Tasks.SQL.Commands;

public class EventRepositorySql
{
    public const string SELECT_ALL_USERS_EVENTS = @"
        SELECT
            *
        FROM
            Events e
        WHERE
            e.user_id = @userId";


    public const string DELETE = @"
        DELETE FROM
            Events e
        WHERE
            e.id = @id";

    public const string SELECT_BY_ID = @"
        SELECT 
            * 
        FROM 
            Events e 
        WHERE
            e.id = @id
        LIMIT 1";


    public const string MODIFY_EVENT_PROCEDURE = "Modify_Event";
}
