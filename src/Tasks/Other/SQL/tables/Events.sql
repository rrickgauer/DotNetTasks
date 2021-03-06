CREATE TABLE `Events` (
  `id` CHAR(36) CHARACTER SET UTF8 COLLATE UTF8_UNICODE_CI NOT NULL,
  `user_id` CHAR(36) CHARACTER SET UTF8 COLLATE UTF8_UNICODE_CI NOT NULL,
  `name` VARCHAR(100) CHARACTER SET UTF8 COLLATE UTF8_UNICODE_CI NOT NULL,
  `description` TEXT CHARACTER SET UTF8 COLLATE UTF8_UNICODE_CI,
  `phone_number` CHAR(10) CHARACTER SET UTF8 COLLATE UTF8_UNICODE_CI DEFAULT NULL,
  `location` CHAR(250) CHARACTER SET UTF8 COLLATE UTF8_UNICODE_CI DEFAULT NULL,
  `starts_on` DATE NOT NULL,
  `ends_on` DATE DEFAULT NULL,
  `starts_at` TIME DEFAULT NULL,
  `ends_at` TIME DEFAULT NULL,
  `frequency` SMALLINT UNSIGNED NOT NULL,
  `separation` INT UNSIGNED DEFAULT '1',
  `created_on` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `recurrence_day` INT DEFAULT NULL,
  `recurrence_week` INT DEFAULT NULL,
  `recurrence_month` INT DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `user_id` (`user_id`),
  KEY `Events_fk_frequencies_idx` (`frequency`),
  CONSTRAINT `Events_fk_frequencies` FOREIGN KEY (`frequency`) REFERENCES `Event_Frequencies` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Events_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=INNODB DEFAULT CHARSET=UTF8MB3 COLLATE=UTF8_UNICODE_CI;
