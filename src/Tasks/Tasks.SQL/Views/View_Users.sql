CREATE ALGORITHM = UNDEFINED DEFINER = `main` @ `%` SQL SECURITY DEFINER VIEW `View_Users` AS
SELECT
    `u`.`id` AS `id`,
    `u`.`email` AS `email`,
    `u`.`password` AS `password`,
    `u`.`created_on` AS `created_on`,
    `u`.`deliver_reminders` AS `deliver_reminders`,
    (
        SELECT
            `v`.`created_on`
        FROM
            `User_Email_Verifications` `v`
        WHERE
            (
                (`v`.`user_id` = `u`.`id`)
                AND (`v`.`email` = `u`.`email`)
                AND (`v`.`confirmed_on` IS NOT NULL)
            )
        LIMIT
            1
    ) AS `email_confirmed_on`
FROM
    `Users` `u`
GROUP BY
    `u`.`id`;