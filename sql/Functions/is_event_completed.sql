DELIMITER $$
CREATE DEFINER=`main`@`%` FUNCTION `is_event_completed`(
    in_event_id CHAR(36),
	in_occurence_date DATE
) RETURNS TINYINT(1)
    READS SQL DATA
BEGIN
    -- Check if the specified event is completed on the specified date
    DECLARE result BOOLEAN;
    DECLARE event_action_type SMALLINT DEFAULT 1;
    
    SELECT 
        EVENT_HAS_ACTION_OCCURENCE(in_event_id, in_occurence_date, event_action_type)
    INTO
        result;
    
    RETURN (result);

END$$
DELIMITER ;
