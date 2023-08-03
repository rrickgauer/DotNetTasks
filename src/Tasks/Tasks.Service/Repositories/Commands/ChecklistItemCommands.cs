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


    public const string Modify = @"
        INSERT INTO Checklist_Items 
        (
            id,
            checklist_id,
            content,
            position,
            completed_on
        )
        VALUES
        (
            @id,
            @checklist_id,
            @content,
            @position,
            @completed_on
        ) AS new_values 
        ON DUPLICATE KEY UPDATE
            content = new_values.content,
            position = new_values.position,
            completed_on = new_values.completed_on";


    public const string Delete = @"
        DELETE FROM
            Checklist_Items
        WHERE
            id = @id";


    public const string CopyItems = @"
        INSERT INTO Checklist_Items 
        (
            id,
            checklist_id,
            content,
            position,
            created_on,
            completed_on
        ) 
        (
            SELECT
                uuid(),
                @target_checklist_id,
                source_checklist.content,
                source_checklist.position,
                source_checklist.created_on,
                source_checklist.completed_on
            FROM
                Checklist_Items source_checklist
            WHERE
                source_checklist.checklist_id = @source_checklist_id
        )";





}
