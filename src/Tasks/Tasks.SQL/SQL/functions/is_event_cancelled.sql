DELIMITER $$

CREATE FUNCTION is_event_cancelled(
    in_event_id CHAR(36),
	in_occurence_date DATE
) 
RETURNS BOOL
READS SQL DATA
BEGIN

    -- Check if the specified event is cancelled on the specified date
    
    DECLARE result BOOLEAN;
    DECLARE event_action_type SMALLINT DEFAULT 2;
    
    SELECT 
        event_has_action_occurence(in_event_id, in_occurence_date, event_action_type) as is_cancelled
    INTO
        result;
    
    RETURN (result);
    
END$$
DELIMITER ;