﻿namespace Tasks.Service.Repositories.Commands;

public sealed class UserEmailVerificationCommands
{
    public const string Modify = @"
        INSERT INTO
            User_Email_Verifications (id, user_id, email, confirmed_on, created_on)
        VALUES
            (@id, @user_id, @email, @confirmed_on, @created_on) AS new_values ON DUPLICATE KEY
        UPDATE
            confirmed_on = new_values.confirmed_on";

    public const string Select = @"
        SELECT
            ev.id AS id,
            ev.user_id AS user_id,
            ev.email AS email,
            ev.confirmed_on AS confirmed_on,
            ev.created_on AS created_on
        FROM
            User_Email_Verifications ev
        WHERE
            ev.id = @id
        LIMIT
            1";
}
