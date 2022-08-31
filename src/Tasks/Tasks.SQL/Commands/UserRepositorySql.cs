namespace Tasks.SQL.Commands
{
    public sealed class UserRepositorySql
    {
        public const string SELECT_FROM_EMAIL_PASSWORD = @"  
            SELECT 
                u.id AS id,
                u.email AS email,
                u.password AS password,
                u.created_on AS created_on
            FROM
                Users u
            WHERE 
                u.email = @email
                AND u.password = @password
            LIMIT 1";


        public const string SELECT_FROM_EMAIL = @"
            SELECT 
                u.id AS id,
                u.email AS email,
                u.password AS password,
                u.created_on AS created_on
            FROM
                Users u
            WHERE 
                u.email = @email
            LIMIT 1";


        public const string SELECT_FROM_ID = @"
            SELECT 
                *
            FROM
                View_Users u
            WHERE 
                u.id = @id
            LIMIT 1";


        public const string UPDATE_PASSWORD = @"
            UPDATE
                Users
            SET
                password = @password
            WHERE
                id = @id";

        public const string MODIFY = @"
            INSERT INTO
                Users (id, email, password, created_on)
            VALUES
                (@id, @email, @password, @created_on) AS new_values ON DUPLICATE KEY
            UPDATE
                email = new_values.email,
                password = new_values.password";


        public const string SELECT_FROM_VIEW = @"
            SELECT
                *
            FROM
                View_Users u
            WHERE
                u.id = @id
            LIMIT
                1";
    }
}
