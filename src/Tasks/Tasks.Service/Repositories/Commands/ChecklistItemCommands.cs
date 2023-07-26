namespace Tasks.Service.Repositories.Commands;

public sealed class ChecklistItemCommands
{

    public const string SelectAllChecklistItems = @"
        SELECT
            i.id AS id,
            i.checklist_id AS checklist_id,
            i.content AS content,
            i.position AS position,
            i.created_on AS created_on,
            i.completed_on AS completed_on
        FROM
            Checklist_Items i
        WHERE
            i.checklist_id = @checklist_id
        ORDER BY
            position ASC";


    public const string SelectSingle = @"
        SELECT
            i.id AS id,
            i.checklist_id AS checklist_id,
            i.content AS content,
            i.position AS position,
            i.created_on AS created_on,
            i.completed_on AS completed_on
        FROM
            Checklist_Items i
        WHERE
            i.id = @id
        LIMIT 
            1";


}
