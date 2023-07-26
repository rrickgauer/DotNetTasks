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


}
