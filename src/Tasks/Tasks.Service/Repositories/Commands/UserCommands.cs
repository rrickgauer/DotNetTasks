namespace Tasks.Service.Repositories.Commands;

public sealed class UserCommands
{
    public const string SelectFromEmailPassword = @"  
        SELECT 
            u.id AS id,
            u.email AS email,
            u.password AS password,
            u.created_on AS created_on,
            u.deliver_reminders AS deliver_reminders 
        FROM
            Users u
        WHERE 
            u.email = @email
            AND u.password = @password
        LIMIT 1";


    public const string SelectFromEmail = @"
        SELECT 
            u.id AS id,
            u.email AS email,
            u.password AS password,
            u.created_on AS created_on,
            u.deliver_reminders AS deliver_reminders
        FROM
            Users u
        WHERE 
            u.email = @email
        LIMIT 1";


    public const string SelectFromId = @"
        SELECT 
            *
        FROM
            View_Users u
        WHERE 
            u.id = @id
        LIMIT 1";

    public const string SelectUsersWithReminders = @"
        SELECT
            *
        FROM
            Users u
        WHERE
            user_gets_reminders(u.id) = TRUE";

    public const string UpdatePassword = @"
        UPDATE
            Users
        SET
            password = @password
        WHERE
            id = @id";


    public const string Modify = @"
        INSERT INTO
            Users (id, email, password, created_on, deliver_reminders)
        VALUES
            (@id, @email, @password, @created_on, @deliver_reminders) AS new_values ON DUPLICATE KEY
        UPDATE
            email = new_values.email,
            deliver_reminders = new_values.deliver_reminders";


    public const string SelectFromView = @"
        SELECT
            *
        FROM
            View_Users u
        WHERE
            u.id = @id
        LIMIT
            1";
}
