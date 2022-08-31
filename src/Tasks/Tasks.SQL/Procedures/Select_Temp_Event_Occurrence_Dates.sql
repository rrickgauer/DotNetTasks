DELIMITER $$
CREATE DEFINER=`main`@`%` PROCEDURE `Select_Temp_Event_Occurrence_Dates`()
BEGIN
    
    /*************************************************************
    This procedure generates the occurences of all events 
    between the given range_start and range_end.
    **************************************************************/
    
    -- now all the event occurences are in the Temp_Event_Occurrence_Dates table
    -- select all those events and match them to the Events meta data
    SELECT 
        teod.event_id AS event_id, 
        e.name AS name,
        e.user_id AS user_id,
        teod.occurs_on AS occurs_on,
        e.starts_at AS starts_at,
        IS_EVENT_COMPLETED(event_id, occurs_on) AS completed,
        IS_EVENT_CANCELLED(event_id, occurs_on) AS cancelled
    FROM
        Temp_Event_Occurrence_Dates teod
        LEFT JOIN Events e ON teod.event_id = e.id
    ORDER BY 
        occurs_on ASC,
        starts_at ASC;
    
    DROP TABLE Temp_Event_Occurrence_Dates;
    
END$$
DELIMITER ;
