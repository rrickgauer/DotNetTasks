
namespace Tasks.Service.Repositories.Commands;

public class EventCommands
{
    public const string SelectAllUsersEvents = @"
        SELECT
            *
        FROM
            Events e
        WHERE
            e.user_id = @userId";


    public const string Delete = @"
        DELETE FROM
            Events e
        WHERE
            e.id = @id";

    public const string SelectById = @"
        SELECT 
            * 
        FROM 
            Events e 
        WHERE
            e.id = @id
        LIMIT 1";


    public const string ModifyEventProcedure = "Modify_Event";
}
