namespace Tasks.Service.Repositories.Commands;

public sealed class ChecklistCommands
{
    public const string SelectAllByUser = @"
        SELECT
            *
        FROM
            View_Checklists c
        WHERE
            c.user_id = @user_id
        ORDER BY
            created_on DESC";


    public const string SelectSingle = @"
        SELECT
            *
        FROM
            View_Checklists c
        WHERE
            c.id = @checklist_id
        LIMIT
            1";


    public const string Save = @"
        INSERT INTO
            Checklists (id, user_id, title, checklist_type_id)
        VALUES
            (@id, @user_id, @title, @checklist_type_id) AS new_values 
        ON DUPLICATE KEY UPDATE
            title = new_values.title,
            checklist_type_id = new_values.checklist_type_id";


    public const string Delete = @"DELETE FROM Checklists WHERE id = @id";


    public const string SelectByCommandLineReference = @"
       SELECT
            v.*
        FROM
            View_Checklists v
        WHERE
            v.command_line_reference = @command_line_reference
        LIMIT
            1;";

}
