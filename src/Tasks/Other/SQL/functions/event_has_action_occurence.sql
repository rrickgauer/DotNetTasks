DELIMITER $$

CREATE FUNCTION event_has_action_occurence (
    in_event_id CHAR(36),
    in_occurence_date DATE,
    in_event_action_type SMALLINT UNSIGNED 
) 
RETURNS BOOL
READS SQL DATA
BEGIN
    -- This method checks if an event has a specified event action type on a date.
    -- Example: check if an event has a completion recorded on July 15th, 2022.
    
    DECLARE num_records INT;
    DECLARE result BOOLEAN;
    
    SELECT 
        COUNT(event_id) c
    INTO 
        num_records 
    FROM 
        Event_Actions ea
    WHERE 
        ea.event_id = in_event_id 
        AND ea.on_date = in_occurence_date
        AND ea.event_action_type_id = in_event_action_type;
    
    IF num_records > 0 THEN
        SET result = TRUE;
    ELSE
        SET result = FALSE;
    END IF;
    
    RETURN (result);
    
END$$
DELIMITER ;