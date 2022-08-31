CREATE VIEW `View_Frequency_Counts` AS
(SELECT 
    (SELECT 
            COUNT(0)
        FROM
            `Events`
        WHERE
            (`Events`.`frequency` = 'ONCE')) AS `count_ONCE`,
    (SELECT 
            COUNT(0)
        FROM
            `Events`
        WHERE
            (`Events`.`frequency` = 'DAILY')) AS `count_DAILY`,
    (SELECT 
            COUNT(0)
        FROM
            `Events`
        WHERE
            (`Events`.`frequency` = 'WEEKLY')) AS `count_WEEKLY`,
    (SELECT 
            COUNT(0)
        FROM
            `Events`
        WHERE
            (`Events`.`frequency` = 'MONTHLY')) AS `count_MONTHLY`,
    (SELECT 
            COUNT(0)
        FROM
            `Events`
        WHERE
            (`Events`.`frequency` = 'YEARLY')) AS `count_YEARLY`
);
