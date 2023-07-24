CREATE TABLE `User_Email_Verifications` (
    `id` CHAR(36) NOT NULL,
    `user_id` CHAR(36) CHARACTER
    SET
        UTF8 COLLATE UTF8_UNICODE_CI NOT NULL,
        `email` CHAR(100) NOT NULL,
        `confirmed_on` DATETIME DEFAULT NULL,
        `created_on` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
        PRIMARY KEY (`id`),
        UNIQUE KEY `id` (`id`),
        KEY `user_id` (`user_id`),
        CONSTRAINT `User_Email_Verifications_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`) ON DELETE CASCADE ON
    UPDATE
        CASCADE
) ENGINE = INNODB DEFAULT CHARSET = UTF8MB4 COLLATE = UTF8MB4_0900_AI_CI;