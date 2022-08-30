CREATE VIEW View_Users AS
SELECT
    u.id AS id,
    u.email AS email,
    u.password AS password,
    u.created_on AS created_on,
    v.confirmed_on AS email_confirmed_on
FROM
    Users u
    LEFT JOIN User_Email_Verifications v ON v.user_id = u.id
    AND v.email = u.email
GROUP BY
    u.id;