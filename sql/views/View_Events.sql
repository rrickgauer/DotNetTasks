CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `main`@`%` 
    SQL SECURITY DEFINER
VIEW `View_Events` AS
    SELECT 
        `e`.`id` AS `id`,
        `e`.`name` AS `name`,
        `e`.`description` AS `description`,
        `e`.`phone_number` AS `phone_number`,
        `e`.`location` AS `location`,
        `e`.`starts_on` AS `starts_on`,
        `e`.`ends_on` AS `ends_on`,
        `e`.`starts_at` AS `starts_at`,
        `e`.`ends_at` AS `ends_at`,
        `e`.`frequency` AS `frequency`,
        `e`.`separation` AS `separation`,
        `e`.`created_on` AS `created_on`,
        `e`.`recurrence_day` AS `recurrence_day`,
        `e`.`recurrence_week` AS `recurrence_week`,
        `e`.`recurrence_month` AS `recurrence_month`
    FROM
        `Events` `e`