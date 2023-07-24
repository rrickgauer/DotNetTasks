CREATE TABLE `Event_Labels` (
  `event_id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `label_id` char(36) NOT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`event_id`,`label_id`),
  KEY `label_id` (`label_id`),
  CONSTRAINT `Event_Labels_ibfk_1` FOREIGN KEY (`event_id`) REFERENCES `Events` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Event_Labels_ibfk_2` FOREIGN KEY (`label_id`) REFERENCES `Labels` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
