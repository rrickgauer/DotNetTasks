CREATE TABLE `Event_Cancelations` (
  `event_id` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `date` date NOT NULL,
  `recorded_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  UNIQUE KEY `uc_event_completetions` (`event_id`,`date`),
  CONSTRAINT `Event_Cancelations_ibfk_1` FOREIGN KEY (`event_id`) REFERENCES `Events` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
