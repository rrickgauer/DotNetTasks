namespace Tasks.Service.Repositories.Commands;

public sealed class ChecklistCommands
{
    public const string SelectAllByUser = @"
        SELECT
        c.id AS id,
        c.user_id AS user_id,
        c.title AS title,
        c.checklist_type_id AS checklist_type_id,
        c.created_on AS created_on,
        c.count_items as count_items
    FROM
        View_Checklists c
    WHERE
        c.user_id = @user_id
    ORDER BY
        created_on DESC";


}
