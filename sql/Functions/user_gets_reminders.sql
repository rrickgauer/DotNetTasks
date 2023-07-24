DELIMITER $$
CREATE DEFINER=`main`@`%` FUNCTION `user_gets_reminders`(
    in_user_id CHAR(36)
) RETURNS tinyint(1)
    READS SQL DATA
BEGIN

    DECLARE user_count INT;
    DECLARE result BOOL;

    SELECT
        count(*) AS count INTO user_count
    FROM
        Users u
    WHERE
        u.id = in_user_id
        AND u.deliver_reminders = TRUE
        AND EXISTS (
            SELECT
                1
            FROM
                User_Email_Verifications e
            WHERE
                e.confirmed_on IS NOT NULL
                AND e.email = u.email
                AND e.user_id = u.id
        );

    IF user_count = 1 THEN
        SET result = TRUE;
    ELSE
        SET result = FALSE;
    END IF;

    RETURN (result);

END$$
DELIMITER ;
