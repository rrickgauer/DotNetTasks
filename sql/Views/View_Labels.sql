CREATE VIEW View_Labels AS
SELECT
    l.id AS id,
    l.user_id AS user_id,
    l.name AS name,
    l.color AS color,
    l.created_on AS created_on
FROM
    Labels l;