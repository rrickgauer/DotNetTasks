namespace Tasks.Service.Repositories.Commands;

public sealed class ChecklistLabelCommands
{
    /// <summary>
    /// @checklist_id
    /// </summary>
    public const string SelectAllLabelsAssignedToChecklist = @"
        SELECT
            *
        FROM
            View_Checklist_Labels cl
        WHERE
            cl.checklist_id = @checklist_id
        ORDER BY
            cl.created_on DESC";


    /// <summary>
    /// @checklist_id
    /// @label_id
    /// @created_on
    /// </summary>
    public const string Replace = @"
        REPLACE INTO Checklist_Labels 
            (checklist_id, label_id, created_on)
        VALUES
            (@checklist_id, @label_id, @created_on)";


    /// <summary>
    /// @checklist_id
    /// @label_id
    /// </summary>
    public const string Delete = @"
        DELETE FROM
            Checklist_Labels
        WHERE
            checklist_id = @checklist_id
            AND label_id = @label_id";
}
