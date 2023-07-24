CREATE TABLE `Users` (
    `id` CHAR(36) COLLATE utf8_unicode_ci NOT NULL,
    `email` VARCHAR(100) COLLATE utf8_unicode_ci NOT NULL,
    `password` VARCHAR(250) COLLATE utf8_unicode_ci NOT NULL,
    `created_on` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `deliver_reminders` BOOLEAN NOT NULL DEFAULT FALSE PRIMARY KEY (`id`),
    UNIQUE KEY `id` (`id`),
    UNIQUE KEY `email` (`email`)
) ENGINE = InnoDB DEFAULT CHARSET = utf8 COLLATE = utf8_unicode_ci;