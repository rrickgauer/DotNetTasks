DELIMITER $$
CREATE FUNCTION get_random_date(
    in_num_days_out INT
) 
RETURNS DATE
DETERMINISTIC
BEGIN
    DECLARE random_num_days INT;
    DECLARE result DATE;
    DECLARE num_days_out INT DEFAULT ABS(in_num_days_out);
    
    -- generate a random number between 0 and the in_num_days_out argument
    SET random_num_days = FLOOR(RAND() * num_days_out);
    
    -- if argument is positive, generate a random day in the future
    -- otherwise get a random day in the past
    IF SIGN(in_num_days_out) = 1 THEN
        SET result = CURRENT_DATE + INTERVAL random_num_days DAY;       -- future
    ELSE
        SET result = CURRENT_DATE - INTERVAL random_num_days DAY;       -- past
    END IF;
    
    RETURN (result);
END$$
DELIMITER ;