DELIMITER $$
CREATE DEFINER=`main`@`%` PROCEDURE `Get_Recurrences_For_Reminders`(
    IN range_start DATE,
    IN range_end DATE
)
BEGIN
    
    DECLARE finished INT DEFAULT 0;
    DECLARE userId CHAR(36);
    
    -- cursor for fetching all the event ids
    DECLARE cursor_users CURSOR FOR 
    SELECT u.id
    FROM Users u
    WHERE
        u.deliver_reminders = TRUE
        AND EXISTS (
            SELECT 1
            FROM User_Email_Verifications e
            WHERE
                e.confirmed_on IS NOT NULL
                AND e.email = u.email
                AND e.user_id = u.id
        );
    
    -- declare NOT FOUND handler
    DECLARE CONTINUE HANDLER 
    FOR NOT FOUND SET finished = 1;
    
    -- this will hold all the event ids and their occurences
    CREATE TEMPORARY TABLE Temp_Event_Occurrence_Dates (
        event_id CHAR(36) NOT NULL,
        occurs_on DATE NOT NULL
    );
    
    OPEN cursor_users;
    
    LOOP_PROCESS_USERS: LOOP
        -- get the next user id
        FETCH cursor_users INTO userId;
        
        -- if no more events exit the loop
        IF finished = 1 THEN
            LEAVE LOOP_PROCESS_USERS;
        END IF;
        
        CALL Get_Recurrences(userId, range_start, range_end, FALSE);
    
    END LOOP LOOP_PROCESS_USERS;
    
    CLOSE cursor_users;
    
    -- now all the event occurences are in the Temp_Event_Occurrence_Dates table
    -- select all those events and match them to the Events meta data
    CALL Select_Temp_Event_Occurrence_Dates();
    
END$$
DELIMITER ;
