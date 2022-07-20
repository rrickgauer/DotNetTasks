DELIMITER $$ 
CREATE PROCEDURE Modify_Event (
    IN in_id CHAR(36),
    IN in_user_id CHAR(36),
    IN in_name VARCHAR(100),
    IN in_description TEXT,
    IN in_phone_number CHAR(10),
    IN in_location CHAR(250),
    IN in_starts_on DATE,
    IN in_ends_on DATE,
    IN in_starts_at TIME,
    IN in_ends_at TIME,
    IN in_frequency ENUM('ONCE', 'DAILY', 'WEEKLY', 'MONTHLY', 'YEARLY'),
    IN in_separation INT UNSIGNED,
    IN in_recurrence_day INT,
    IN in_recurrence_week INT,
    IN in_recurrence_month INT
) BEGIN
INSERT INTO
    Events (
        id,
        user_id,
        name,
        description,
        phone_number,
        location,
        starts_on,
        ends_on,
        starts_at,
        ends_at,
        frequency,
        separation,
        recurrence_day,
        recurrence_week,
        recurrence_month
    )
VALUES
    (
        in_id,
        in_user_id,
        in_name,
        in_description,
        in_phone_number,
        in_location,
        in_starts_on,
        in_ends_on,
        in_starts_at,
        in_ends_at,
        in_frequency,
        in_separation,
        in_recurrence_day,
        in_recurrence_week,
        in_recurrence_month
    ) AS new_values ON DUPLICATE KEY
UPDATE
    name = new_values.name,
    description = new_values.description,
    phone_number = new_values.phone_number,
    location = new_values.location,
    starts_on = new_values.starts_on,
    ends_on = new_values.ends_on,
    starts_at = new_values.starts_at,
    ends_at = new_values.ends_at,
    frequency = new_values.frequency,
    separation = new_values.separation,
    recurrence_day = new_values.recurrence_day,
    recurrence_week = new_values.recurrence_week,
    recurrence_month = new_values.recurrence_month;

END$$ 
DELIMITER ;