namespace Tasks.Service.Repositories.Commands;

public sealed class LabelCommands
{
    /// <summary>
    /// Select all the user labels
    /// Order by name
    /// </summary>
    public const string SelectAllByUserId = @"
        SELECT
            *
        FROM
            View_Labels l
        WHERE
            l.user_id = @user_id
        ORDER BY
            l.name ASC";


    public const string SelectById = @"
        SELECT
            *
        FROM
            View_Labels l
        WHERE
            l.id = @id
        LIMIT 
            1";

    /// <summary>
    /// Modify a label
    /// </summary>
    public const string Modify = @"
        INSERT INTO
            Labels (id, user_id, name, color, created_on)
        VALUES
            (@id, @user_id, @name, @color, @created_on) AS new_values 
        ON DUPLICATE KEY UPDATE
            name = new_values.name,
            color = new_values.color";


    public const string DeleteByIdAndUserId = @"
        DELETE FROM 
            Labels
        WHERE 
            id = @id
            AND user_id = @user_id";
}
