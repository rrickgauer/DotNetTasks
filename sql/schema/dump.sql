-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: 104.225.208.163    Database: Tasks_Dev
-- ------------------------------------------------------
-- Server version	8.0.28-0ubuntu0.20.04.3

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `Tasks_Dev`
--

USE `Tasks_Dev`;

--
-- Table structure for table `Checklist_Items`
--

DROP TABLE IF EXISTS `Checklist_Items`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Checklist_Items` (
  `id` char(36) NOT NULL,
  `checklist_id` char(36) NOT NULL,
  `content` char(150) DEFAULT NULL,
  `position` int unsigned NOT NULL DEFAULT '0',
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `completed_on` timestamp NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `checklist_id` (`checklist_id`),
  CONSTRAINT `Checklist_Items_ibfk_1` FOREIGN KEY (`checklist_id`) REFERENCES `Checklists` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`main`@`%`*/ /*!50003 TRIGGER `Checklist_Items_After_Insert` AFTER INSERT ON `Checklist_Items` FOR EACH ROW BEGIN

    INSERT INTO 
        Checklist_Items_Reference (checklist_item_id) 
    VALUES 
        (NEW.id);

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `Checklist_Items_Reference`
--

DROP TABLE IF EXISTS `Checklist_Items_Reference`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Checklist_Items_Reference` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `checklist_item_id` char(36) NOT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `checklist_item_id` (`checklist_item_id`),
  CONSTRAINT `Checklist_Items_Reference_ibfk_1` FOREIGN KEY (`checklist_item_id`) REFERENCES `Checklist_Items` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=514 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Checklist_Label_Assignments`
--

DROP TABLE IF EXISTS `Checklist_Label_Assignments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Checklist_Label_Assignments` (
  `checklist_id` char(36) NOT NULL,
  `label_id` char(36) NOT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`checklist_id`,`label_id`),
  KEY `label_id` (`label_id`),
  CONSTRAINT `Checklist_Label_Assignments_ibfk_1` FOREIGN KEY (`checklist_id`) REFERENCES `Checklists` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Checklist_Label_Assignments_ibfk_2` FOREIGN KEY (`label_id`) REFERENCES `Labels` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Checklist_Labels`
--

DROP TABLE IF EXISTS `Checklist_Labels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Checklist_Labels` (
  `checklist_id` char(36) NOT NULL,
  `label_id` char(36) NOT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`checklist_id`,`label_id`),
  KEY `label_id` (`label_id`),
  CONSTRAINT `Checklist_Labels_ibfk_1` FOREIGN KEY (`checklist_id`) REFERENCES `Checklists` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Checklist_Labels_ibfk_2` FOREIGN KEY (`label_id`) REFERENCES `Labels` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Checklist_Types`
--

DROP TABLE IF EXISTS `Checklist_Types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Checklist_Types` (
  `id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` char(20) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Checklists`
--

DROP TABLE IF EXISTS `Checklists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Checklists` (
  `id` char(36) NOT NULL,
  `user_id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `title` char(100) DEFAULT NULL,
  `checklist_type_id` smallint unsigned NOT NULL DEFAULT '1',
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `user_id` (`user_id`),
  KEY `Checklists_ibfk_2_idx` (`checklist_type_id`),
  CONSTRAINT `Checklists_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Checklists_ibfk_2` FOREIGN KEY (`checklist_type_id`) REFERENCES `Checklist_Types` (`id`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`main`@`%`*/ /*!50003 TRIGGER `After_Checklists_Insert` AFTER INSERT ON `Checklists` FOR EACH ROW BEGIN

Insert into Checklists_Reference (checklist_id) values (NEW.id);

END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `Checklists_Reference`
--

DROP TABLE IF EXISTS `Checklists_Reference`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Checklists_Reference` (
  `id` int unsigned NOT NULL AUTO_INCREMENT,
  `checklist_id` char(36) NOT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `checklist_id` (`checklist_id`),
  CONSTRAINT `Checklists_Reference_ibfk_1` FOREIGN KEY (`checklist_id`) REFERENCES `Checklists` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=260 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Event_Action_Types`
--

DROP TABLE IF EXISTS `Event_Action_Types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Event_Action_Types` (
  `id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` char(30) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Event_Actions`
--

DROP TABLE IF EXISTS `Event_Actions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Event_Actions` (
  `event_id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `on_date` date NOT NULL,
  `event_action_type_id` smallint unsigned NOT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`event_id`,`on_date`,`event_action_type_id`),
  KEY `event_action_type_id` (`event_action_type_id`),
  CONSTRAINT `Event_Actions_ibfk_1` FOREIGN KEY (`event_id`) REFERENCES `Events` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Event_Actions_ibfk_2` FOREIGN KEY (`event_action_type_id`) REFERENCES `Event_Action_Types` (`id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Event_Frequencies`
--

DROP TABLE IF EXISTS `Event_Frequencies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Event_Frequencies` (
  `id` smallint unsigned NOT NULL AUTO_INCREMENT,
  `name` char(30) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `name` (`name`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Event_Labels`
--

DROP TABLE IF EXISTS `Event_Labels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Event_Labels` (
  `event_id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `label_id` char(36) NOT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`event_id`,`label_id`),
  KEY `label_id` (`label_id`),
  CONSTRAINT `Event_Labels_ibfk_1` FOREIGN KEY (`event_id`) REFERENCES `Events` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Event_Labels_ibfk_2` FOREIGN KEY (`label_id`) REFERENCES `Labels` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Event_Notes`
--

DROP TABLE IF EXISTS `Event_Notes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Event_Notes` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `event_id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `created_on` datetime NOT NULL,
  `content` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `event_id` (`event_id`),
  CONSTRAINT `Event_Notes_ibfk_1` FOREIGN KEY (`event_id`) REFERENCES `Events` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Events`
--

DROP TABLE IF EXISTS `Events`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Events` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `user_id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `name` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci,
  `phone_number` char(10) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `location` char(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  `starts_on` date NOT NULL,
  `ends_on` date DEFAULT NULL,
  `starts_at` time DEFAULT NULL,
  `ends_at` time DEFAULT NULL,
  `frequency` smallint unsigned NOT NULL,
  `separation` int unsigned DEFAULT '1',
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `recurrence_day` int DEFAULT NULL,
  `recurrence_week` int DEFAULT NULL,
  `recurrence_month` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `user_id` (`user_id`),
  KEY `Events_fk_frequencies_idx` (`frequency`),
  CONSTRAINT `Events_fk_frequencies` FOREIGN KEY (`frequency`) REFERENCES `Event_Frequencies` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Events_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Labels`
--

DROP TABLE IF EXISTS `Labels`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Labels` (
  `id` char(36) NOT NULL,
  `user_id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `name` char(50) NOT NULL,
  `color` char(7) DEFAULT '#ffffff',
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `Labels_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `User_Email_Verifications`
--

DROP TABLE IF EXISTS `User_Email_Verifications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `User_Email_Verifications` (
  `id` char(36) NOT NULL,
  `user_id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `email` char(100) NOT NULL,
  `confirmed_on` datetime DEFAULT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `User_Email_Verifications_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `Users` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Users` (
  `id` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `email` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `password` varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `created_on` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deliver_reminders` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Temporary view structure for view `View_Checklist_Items`
--

DROP TABLE IF EXISTS `View_Checklist_Items`;
/*!50001 DROP VIEW IF EXISTS `View_Checklist_Items`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Checklist_Items` AS SELECT 
 1 AS `id`,
 1 AS `command_line_reference`,
 1 AS `checklist_id`,
 1 AS `content`,
 1 AS `position`,
 1 AS `created_on`,
 1 AS `completed_on`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Checklist_Labels`
--

DROP TABLE IF EXISTS `View_Checklist_Labels`;
/*!50001 DROP VIEW IF EXISTS `View_Checklist_Labels`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Checklist_Labels` AS SELECT 
 1 AS `checklist_id`,
 1 AS `checklist_user_id`,
 1 AS `checklist_title`,
 1 AS `checklist_type_id`,
 1 AS `checklist_created_on`,
 1 AS `label_id`,
 1 AS `label_user_id`,
 1 AS `label_name`,
 1 AS `label_color`,
 1 AS `label_created_on`,
 1 AS `created_on`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Checklists`
--

DROP TABLE IF EXISTS `View_Checklists`;
/*!50001 DROP VIEW IF EXISTS `View_Checklists`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Checklists` AS SELECT 
 1 AS `id`,
 1 AS `command_line_reference`,
 1 AS `user_id`,
 1 AS `title`,
 1 AS `checklist_type_id`,
 1 AS `created_on`,
 1 AS `count_items`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Events`
--

DROP TABLE IF EXISTS `View_Events`;
/*!50001 DROP VIEW IF EXISTS `View_Events`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Events` AS SELECT 
 1 AS `id`,
 1 AS `name`,
 1 AS `description`,
 1 AS `phone_number`,
 1 AS `location`,
 1 AS `starts_on`,
 1 AS `ends_on`,
 1 AS `starts_at`,
 1 AS `ends_at`,
 1 AS `frequency`,
 1 AS `separation`,
 1 AS `created_on`,
 1 AS `recurrence_day`,
 1 AS `recurrence_week`,
 1 AS `recurrence_month`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Frequency_Counts`
--

DROP TABLE IF EXISTS `View_Frequency_Counts`;
/*!50001 DROP VIEW IF EXISTS `View_Frequency_Counts`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Frequency_Counts` AS SELECT 
 1 AS `count_ONCE`,
 1 AS `count_DAILY`,
 1 AS `count_WEEKLY`,
 1 AS `count_MONTHLY`,
 1 AS `count_YEARLY`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Labels`
--

DROP TABLE IF EXISTS `View_Labels`;
/*!50001 DROP VIEW IF EXISTS `View_Labels`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Labels` AS SELECT 
 1 AS `id`,
 1 AS `user_id`,
 1 AS `name`,
 1 AS `color`,
 1 AS `created_on`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `View_Users`
--

DROP TABLE IF EXISTS `View_Users`;
/*!50001 DROP VIEW IF EXISTS `View_Users`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_Users` AS SELECT 
 1 AS `id`,
 1 AS `email`,
 1 AS `password`,
 1 AS `created_on`,
 1 AS `deliver_reminders`,
 1 AS `email_confirmed_on`*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping events for database 'Tasks_Dev'
--

--
-- Dumping routines for database 'Tasks_Dev'
--
/*!50003 DROP FUNCTION IF EXISTS `event_has_action_occurence` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `event_has_action_occurence`(
    in_event_id CHAR(36),
    in_occurence_date DATE,
    in_event_action_type SMALLINT UNSIGNED 
) RETURNS tinyint(1)
    READS SQL DATA
BEGIN
    -- This method checks if an event has a specified event action type on a date.
    -- Example: check if an event has a completion recorded on July 15th, 2022.
    
    DECLARE num_records INT;
    DECLARE result BOOLEAN;
    
    SELECT 
        COUNT(event_id) c
    INTO 
        num_records 
    FROM 
        Event_Actions ea
    WHERE 
        ea.event_id = in_event_id 
        AND ea.on_date = in_occurence_date
        AND ea.event_action_type_id = in_event_action_type;
    
    IF num_records > 0 THEN
        SET result = TRUE;
    ELSE
        SET result = FALSE;
    END IF;
    
    RETURN (result);
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `format_date_display` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `format_date_display`(
	original_date DATE
) RETURNS char(100) CHARSET utf8mb3 COLLATE utf8_unicode_ci
    DETERMINISTIC
BEGIN
	DECLARE date_formatted VARCHAR(100);
    SET date_formatted = DATE_FORMAT(original_date, "%c/%d/%Y");
    RETURN (date_formatted);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `format_time_display` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `format_time_display`(
	original_date DATETIME
) RETURNS char(100) CHARSET utf8mb3 COLLATE utf8_unicode_ci
    DETERMINISTIC
BEGIN
	DECLARE date_formatted VARCHAR(100);
    SET date_formatted = DATE_FORMAT(original_date, "%l:%i %p");
    RETURN (date_formatted);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_first_date_in_the_month` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_first_date_in_the_month`(
	original_date DATE
) RETURNS date
    DETERMINISTIC
BEGIN
    DECLARE first_date DATE;
	DECLARE day_of_month INT;
    
    SET first_date = original_date;
    
	SET day_of_month = EXTRACT(DAY FROM first_date) - 1;
	SET first_date = date_sub(first_date, INTERVAL day_of_month DAY);

    
    RETURN (first_date);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_first_monthday_date` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_first_monthday_date`(
    range_start DATE,
    event_starts_on date,
    event_seperation int,
    event_recurrence_day int
) RETURNS date
    DETERMINISTIC
BEGIN
	
    /***************************************************************************
    get_first_monthday_date retrieves the first date a "MonthDay" occurence
    can happen on/after the given range_start date
    ****************************************************************************/
    DECLARE first_date DATE;
    DECLARE day_of_month INT;

    SET first_date = event_starts_on;

    -- first, we need to set the first_date to the first day of the month
    SET day_of_month = EXTRACT(DAY FROM first_date);
    SET first_date = date_sub(first_date, INTERVAL day_of_month DAY);

    -- now we need to add the event_recurrence_day to the first_date
    -- to get it to be on the specific day of the month it occurs
    SET first_date = DATE_ADD(first_date, INTERVAL event_recurrence_day DAY);

    -- we need to get the first_date to first acceptable month the event can occur at
    -- keep adding the event_seperation of month intervals until the start_date is
    -- equal to or 1 event_seperation month interval past the range_start
    WHILE first_date < range_start DO
        SET first_date = DATE_ADD(first_date, INTERVAL event_seperation MONTH);
    END WHILE;

    -- all done!
    RETURN (first_date);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_first_monthweek_date` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_first_monthweek_date`(
    range_start DATE,
    event_starts_on date,
    event_seperation int,
    event_recurrence_week int,
    event_recurrence_day int
) RETURNS date
    DETERMINISTIC
BEGIN
	
    /***************************************************************************
    get_first_monthweek_date retrieves the first date a "MonthWeek" type event
    can occur on/after the given range_start date
    ****************************************************************************/
    DECLARE first_date DATE;
    DECLARE day_of_month INT;
    DECLARE day_of_week int;
    DECLARE rangeStartFirstDayOfMonth DATE;
	
     SET first_date = event_starts_on;
    
    -- we need to get the first_date to first acceptable month the event can occur at
    -- keep adding the event_seperation of month intervals until the start_date is
    -- equal to or 1 event_seperation month interval past the range_start
    WHILE first_date < get_first_date_in_the_month(range_start) DO
		SET first_date = DATE_ADD(first_date, INTERVAL event_seperation MONTH);
    END WHILE;
    
    -- now that the first_date is in the first acceptable month the event can occur we need to set the first_date's day value
    -- first, we need to set the first_date to the first day of the month
    SET first_date = get_first_date_in_the_month(first_date);
	
    while DAYOFWEEK(first_date) - 1 <> event_recurrence_day do
		set first_date = date_add(first_date, interval 1 day);
	end while;
    
	-- set the date to the recurrence week value
	SET first_date = DATE_ADD(first_date, INTERVAL event_recurrence_week - 1 WEEK);
    
     return (first_date);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_first_yearly_date` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_first_yearly_date`(
    range_start DATE,
    event_starts_on DATE,
    event_seperation INT,
    event_recurrence_month INT,
    event_recurrence_week INT,
    event_recurrence_day INT
) RETURNS date
    DETERMINISTIC
BEGIN
	
    /***************************************************************************
    get_first_yearly_date retrieves the first date a "YEARLY" type event
    can occur on/after the given range_start date
    ****************************************************************************/
    DECLARE first_date DATE;
	DECLARE event_starts_on_month INT;
    DECLARE num_days_in_month INT;
	
	SET first_date = event_starts_on;
    
    -- is the recurrence month different than the month the event started on?
    -- if so, we need to set it to that
    IF event_recurrence_month IS NOT NULL THEN
        -- set the date to the specified event_recurrence_month
        SET event_starts_on_month = EXTRACT(MONTH FROM first_date);
        SET first_date = DATE_SUB(first_date, INTERVAL event_starts_on_month MONTH);
        SET first_date = DATE_ADD(first_date, INTERVAL event_recurrence_month MONTH);	-- now the date's month and year is set
    END IF;
    
    -- now we need to get the date into the range
    -- but following the event_seperation rule
    -- so we keep adding event_seperation year intervals till it reaches the range_start
    WHILE first_date < range_start DO
		SET first_date = DATE_ADD(first_date, INTERVAL event_seperation YEAR);
    END WHILE;
    
    -- now we need to set the date's day value
    IF event_recurrence_week IS NOT NULL 
    AND event_recurrence_day IS NOT NULL THEN
		SET first_date = GET_NEXT_MONTHWEEK_DATE(first_date, event_recurrence_week, event_recurrence_day);
	ELSE
		SET first_date = GET_FIRST_DATE_IN_THE_MONTH(first_date);
		
        SET num_days_in_month = DAY(LAST_DAY(first_date));	-- number of days in the month
        
        -- if the specified day of the month is larger than the number of actual days in the month
        -- (event_recurrence_day is 31, but the month is november (only has 30 days)) 
        -- set the day to the last day in the month
        IF event_recurrence_day > num_days_in_month THEN
			SET first_date = DATE_ADD(first_date, INTERVAL num_days_in_month DAY);
		ELSE
			SET first_date = DATE_ADD(first_date, INTERVAL event_recurrence_day DAY);
            SET first_date = DATE_ADD(first_date, INTERVAL -1 DAY);
        END IF;
        
    END IF;
    
    -- sometimes the date is still just before the range start
    -- so add 1 more seperation interval to it
    IF first_date < range_start THEN
		SET first_date = DATE_ADD(first_date, INTERVAL event_seperation YEAR);
    END IF;
    
    
     RETURN (first_date);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_next_monthweek_date` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_next_monthweek_date`( 
	in_next_date DATE,
    event_recurrence_week INT,
    event_recurrence_day INT
) RETURNS date
    DETERMINISTIC
BEGIN
	DECLARE next_date DATE;
    SET next_date = in_next_date;

	-- set it to the first of the month	
	SET next_date = GET_FIRST_DATE_IN_THE_MONTH(next_date);

	-- move it to the first day that's equal to the event_recurrence value
	WHILE DAYOFWEEK(next_date) - 1 <> event_recurrence_day DO
		SET next_date = DATE_ADD(next_date, INTERVAL 1 DAY);
	END WHILE;

	-- set the date to the recurrence week value
	SET next_date = DATE_ADD(next_date, INTERVAL event_recurrence_week - 1 WEEK);
	
    RETURN(next_date);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_random_date` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_random_date`(
    in_num_days_out INT
) RETURNS date
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
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_start_date_daily` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_start_date_daily`(
	range_start DATE,
    starts_on DATE,
    event_seperation INT
) RETURNS date
    DETERMINISTIC
BEGIN
    DECLARE first_date DATE;
    SET first_date = starts_on;
    
    -- add seperation day intervals until the date is greater than or equal to the range start
    WHILE first_date < range_start DO
        SET first_date = DATE_ADD(first_date, INTERVAL event_seperation DAY);
    END WHILE;
    
    RETURN (first_date);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_start_date_monthly` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_start_date_monthly`(
	starts_on DATE,
    occurrence_day INT
) RETURNS date
    DETERMINISTIC
BEGIN
    DECLARE firstInterval INT;
    DECLARE startDate DATE;
	
    SET firstInterval = occurrence_day - EXTRACT(DAY FROM starts_on);
	
    SET startDate = DATE_ADD(starts_on, INTERVAL firstInterval DAY);
    
    RETURN (startDate);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_start_date_monthly2` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_start_date_monthly2`(
	starts_on DATE,
    occurrence_day INT
) RETURNS date
    DETERMINISTIC
BEGIN
    DECLARE firstInterval INT;
    DECLARE startDate DATE;
	
    SET firstInterval = (12 + (occurrence_day) - ((DAYOFWEEK(starts_on) - 1) % 12));
	
    SET startDate = DATE_ADD(starts_on, INTERVAL firstInterval MONTH);
    
    RETURN (startDate);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `get_start_date_weekly` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `get_start_date_weekly`(
	range_start DATE,
    event_starts_on DATE,
    event_seperation INT,
    event_recurrence_day INT
) RETURNS date
    DETERMINISTIC
BEGIN
	
    DECLARE first_date  DATE;
    DECLARE day_of_week INT;
    SET first_date = event_starts_on;
    
    WHILE first_date < range_start DO
		SET first_date = DATE_ADD(first_date, INTERVAL event_seperation WEEK);
    END WHILE;

    -- set the date's dayofweek to 0
    SET day_of_week = DAYOFWEEK(first_date) - 1;
    SET first_date = DATE_SUB(first_date, INTERVAL day_of_week DAY);
    
    
    SET first_date = DATE_ADD(first_date, INTERVAL event_recurrence_day DAY);
    
    IF first_date < range_start THEN
		SET first_date = DATE_ADD(first_date, INTERVAL event_seperation WEEK);
	END IF;
    
    RETURN (first_date);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `intervals_between` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `intervals_between`( 
	date_start DATE,
    date_end DATE,
    frequency VARCHAR(20)
) RETURNS float
    DETERMINISTIC
BEGIN

	DECLARE count FLOAT;
    
    IF date_start > date_end THEN
		SET count = 0;
        RETURN (count);
	END IF;
    
    
    CASE frequency
		WHEN 'DAILY' THEN
			SET count = ABS(TIMESTAMPDIFF(DAY, date_start , date_end));
		WHEN 'WEEKLY' THEN
			SET count = ABS(TIMESTAMPDIFF(WEEK, date_start , date_end));
		WHEN 'MONTHLY' THEN
			SET count = ABS(TIMESTAMPDIFF(MONTH, date_start , date_end));
		WHEN 'YEARLY' THEN
			SET count = ABS(TIMESTAMPDIFF(YEAR, date_start , date_end));
	END CASE;
    
    RETURN (count);


END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `is_event_cancelled` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `is_event_cancelled`(
    in_event_id CHAR(36),
	in_occurence_date DATE
) RETURNS tinyint(1)
    READS SQL DATA
BEGIN

    -- Check if the specified event is cancelled on the specified date
    
    DECLARE result BOOLEAN;
    DECLARE event_action_type SMALLINT DEFAULT 2;
    
    SELECT 
        event_has_action_occurence(in_event_id, in_occurence_date, event_action_type) as is_cancelled
    INTO
        result;
    
    RETURN (result);
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `is_event_completed` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `is_event_completed`(
    in_event_id CHAR(36),
	in_occurence_date DATE
) RETURNS tinyint(1)
    READS SQL DATA
BEGIN
    -- Check if the specified event is completed on the specified date
    DECLARE result BOOLEAN;
    DECLARE event_action_type SMALLINT DEFAULT 1;
    
    SELECT 
        event_has_action_occurence(in_event_id, in_occurence_date, event_action_type)
    INTO
        result;
    
    RETURN (result);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP FUNCTION IF EXISTS `user_gets_reminders` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` FUNCTION `user_gets_reminders`(
	in_user_id CHAR(36)
) RETURNS tinyint(1)
    READS SQL DATA
BEGIN

	DECLARE user_count INT;
	DECLARE result BOOL;

	SELECT
		count(*) AS count INTO user_count
	FROM
		Users u
	WHERE
		u.id = in_user_id
		AND u.deliver_reminders = TRUE
		AND EXISTS (
			SELECT
				1
			FROM
				User_Email_Verifications e
			WHERE
				e.confirmed_on IS NOT NULL
				AND e.email = u.email
				AND e.user_id = u.id
		);

	IF user_count = 1 THEN
		SET result = TRUE;
	ELSE
		SET result = FALSE;
	END IF;

	RETURN (result);

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Get_Event_Recurrences` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` PROCEDURE `Get_Event_Recurrences`(
	IN event_id CHAR(36),
    IN range_start DATE,
    IN range_end DATE,
    IN return_results BOOLEAN
)
SP: BEGIN
	/************************************************************************
    This procedure generates all of the dates an event will occur on 
    between the given range_start and range_end days. 
    *************************************************************************/
    
    -- event frequency symbolic constants
	DECLARE C_FREQUENCY_ONCE SMALLINT UNSIGNED DEFAULT 1;
    DECLARE C_FREQUENCY_DAILY SMALLINT UNSIGNED DEFAULT 2;
    DECLARE C_FREQUENCY_WEEKLY SMALLINT UNSIGNED DEFAULT 3;
    DECLARE C_FREQUENCY_MONTHLY SMALLINT UNSIGNED DEFAULT 4;
    DECLARE C_FREQUENCY_YEARLY SMALLINT UNSIGNED DEFAULT 5;

	-- Event row fields
    DECLARE event_starts_on DATE;
    DECLARE event_ends_on DATE;
    DECLARE event_separation INT;
    DECLARE event_frequency SMALLINT UNSIGNED;    
    DECLARE event_recurrence_day INT;
    DECLARE event_recurrence_week INT;
    DECLARE event_recurrence_month INT;
    
    -- my fields
    DECLARE first_date DATE;
    DECLARE next_date DATE;
    DECLARE num_intervals INT;
    DECLARE helperInt INT;
    DECLARE month_start DATE;
    
    -- get all the event data from the Events table
	SELECT 
		e.starts_on, e.ends_on, e.separation, e.frequency, e.recurrence_day, e.recurrence_week, e.recurrence_month
	INTO event_starts_on , event_ends_on , event_separation , event_frequency , event_recurrence_day, event_recurrence_week, event_recurrence_month 
	FROM 
		Events e
	WHERE
		e.id = event_id;
    
    -- make sure that range_start and range_end fall within the events starts_on and ends_on fields
    IF range_start > event_ends_on THEN
		LEAVE SP;
	ELSEIF range_end < event_starts_on THEN
		LEAVE SP;
	ELSEIF event_frequency = C_FREQUENCY_ONCE AND event_starts_on < range_start THEN
		LEAVE SP;
	END IF;
    
    -- initialize the first_date to the start of the range
	SET first_date = range_start;	
    
    -- need to get the first date the event can occur on between the ranges following the event_separation
    IF event_frequency = C_FREQUENCY_WEEKLY THEN
		SET first_date = GET_START_DATE_WEEKLY(range_start, event_starts_on, event_separation, event_recurrence_day);
	ELSEIF event_frequency = C_FREQUENCY_DAILY THEN
		SET first_date = GET_START_DATE_DAILY(range_start, event_starts_on, event_separation);
	ELSEIF event_frequency = C_FREQUENCY_MONTHLY THEN
		IF event_recurrence_week IS NULL 
        AND event_recurrence_day IS NOT NULL THEN
			SET first_date = GET_FIRST_MONTHDAY_DATE(range_start, event_starts_on, event_separation, event_recurrence_day);
		ELSE
			SET first_date = GET_FIRST_MONTHWEEK_DATE(range_start, event_starts_on, event_separation, event_recurrence_week, event_recurrence_day);
		END IF;
	ELSEIF event_frequency = C_FREQUENCY_YEARLY THEN
		SET first_date = GET_FIRST_YEARLY_DATE(range_start, event_starts_on, event_separation, event_recurrence_month, event_recurrence_week, event_recurrence_day);
	ELSE	-- ONCE
		SET first_date = event_starts_on;
    END IF;
    
    -- set the next date to the first date
    SET next_date = first_date;
	
	-- create the temporary table if it does not exist
	CREATE TEMPORARY TABLE IF NOT EXISTS Temp_Event_Occurrence_Dates (
		event_id CHAR(36) NOT NULL,
		occurs_on DATE NOT NULL
	);

    
    SET num_intervals = event_separation;
    
    -- generate the date the event occurs on
	WHILE 
		next_date <= range_end AND
        next_date <= event_ends_on
	DO
		-- add the date the result set
		INSERT INTO Temp_Event_Occurrence_Dates VALUES (event_id, next_date);
        
        -- get the next date depending on the event frequency
        IF event_frequency = C_FREQUENCY_WEEKLY THEN
			SET next_date = DATE_ADD(next_date, INTERVAL num_intervals WEEK);
		ELSEIF event_frequency = C_FREQUENCY_DAILY THEN
			SET next_date = DATE_ADD(next_date, INTERVAL num_intervals DAY);
        ELSEIF event_frequency = C_FREQUENCY_MONTHLY THEN
			SET next_date = DATE_ADD(next_date, INTERVAL num_intervals MONTH);
            -- check if the recurrence is one that is a MONTHWEEK
            IF event_recurrence_week IS NOT NULL AND event_recurrence_day IS NOT NULL THEN
				SET next_date = GET_NEXT_MONTHWEEK_DATE(next_date, event_recurrence_week, event_recurrence_day);
            END IF;
		ELSEIF event_frequency = C_FREQUENCY_YEARLY THEN
            SET next_date = DATE_ADD(next_date, INTERVAL event_separation YEAR);
			-- check if the recurrence is one that is a MONTHWEEK
            IF event_recurrence_week IS NOT NULL AND event_recurrence_day IS NOT NULL THEN
				SET next_date = GET_NEXT_MONTHWEEK_DATE(next_date, event_recurrence_week, event_recurrence_day);
            END IF;
		ELSE	-- once
            SET next_date = DATE_ADD(event_ends_on, INTERVAL 1 YEAR);
		END IF;
    END WHILE;
	
	-- if the caller wants to return the results,
    -- then we need to make the temp table to return the event's occurence dates
    IF return_results = TRUE THEN
		CALL Select_Temp_Event_Occurrence_Dates();
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Get_Recurrences` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` PROCEDURE `Get_Recurrences`(
    IN user_id CHAR(36),
    IN range_start DATE,
    IN range_end DATE,
    IN return_results BOOL
)
BEGIN
    
    /*************************************************************
    This procedure generates the occurences of all events 
    between the given range_start and range_end.
    **************************************************************/
    
    DECLARE finished INT DEFAULT 0;
    DECLARE eventID CHAR(36);
    
    -- cursor for fetching all the event ids
    DECLARE cursor_events CURSOR 
    FOR SELECT e.id 
    FROM Events e 
    WHERE e.user_id = user_id;
    
    -- declare NOT FOUND handler
    DECLARE CONTINUE HANDLER 
    FOR NOT FOUND SET finished = 1;
    
    -- this will hold all the event ids and their occurences
    CREATE TEMPORARY TABLE IF NOT EXISTS Temp_Event_Occurrence_Dates (
        event_id CHAR(36) NOT NULL,
        occurs_on DATE NOT NULL
    );
    
    OPEN cursor_events;
    
    -- for every event id, generate the event's occurrence dates
    LOOP_PROCESS_EVENTS: LOOP
        -- get the next event_id
        FETCH cursor_events INTO eventID;
        
        -- if no more events exit the loop
        IF finished = 1 THEN
            LEAVE LOOP_PROCESS_EVENTS;
        END IF;
        
        CALL Get_Event_Recurrences(eventID, range_start, range_end, FALSE);
    
    END LOOP LOOP_PROCESS_EVENTS;
    CLOSE cursor_events;
    
    
    -- now all the event occurences are in the Temp_Event_Occurrence_Dates table
    -- select all those events and match them to the Events meta data
    IF return_results THEN
        CALL Select_Temp_Event_Occurrence_Dates();
    END IF;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Get_Recurrences_For_Reminders` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Initialize_Checklists_Reference_Entries` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` PROCEDURE `Initialize_Checklists_Reference_Entries`()
BEGIN
	
INSERT INTO
    Checklists_Reference (checklist_id)
SELECT
    c.id
FROM
    Checklists c
WHERE
    NOT EXISTS (
        SELECT
            cr.checklist_id
        FROM
            Checklists_Reference cr
        WHERE
            cr.checklist_id = c.id
    )
ORDER BY c.created_on ASC;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Initialize_Checklist_Item_Reference_Entries` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` PROCEDURE `Initialize_Checklist_Item_Reference_Entries`()
BEGIN
    

INSERT INTO Checklist_Items_Reference (checklist_item_id)
(
    select c.id
    from Checklist_Items c
    where c.id not in 
    (
        SELECT cir.checklist_item_id
        from Checklist_Items_Reference cir
        where cir.checklist_item_id = c.id
    )
);


    
  
  
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Modify_Event` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`main`@`%` PROCEDURE `Modify_Event`(
    IN in_id CHAR(36),
    IN in_user_id CHAR(36),
    IN in_name VARCHAR(100),
    IN in_description TEXT,
    IN in_phone_number CHAR(10),
    IN in_location CHAR(250),
    IN in_starts_on DATE,
    IN in_ends_on DATE,
    IN in_starts_at TIME,
    IN in_ends_at TIME,
    IN in_frequency SMALLINT UNSIGNED,
    IN in_separation INT UNSIGNED,
    IN in_recurrence_day INT,
    IN in_recurrence_week INT,
    IN in_recurrence_month INT
)
BEGIN
INSERT INTO
    Events (
        id,
        user_id,
        name,
        description,
        phone_number,
        location,
        starts_on,
        ends_on,
        starts_at,
        ends_at,
        frequency,
        separation,
        recurrence_day,
        recurrence_week,
        recurrence_month
    )
VALUES
    (
        in_id,
        in_user_id,
        in_name,
        in_description,
        in_phone_number,
        in_location,
        in_starts_on,
        in_ends_on,
        in_starts_at,
        in_ends_at,
        in_frequency,
        in_separation,
        in_recurrence_day,
        in_recurrence_week,
        in_recurrence_month
    ) AS new_values ON DUPLICATE KEY
UPDATE
    name = new_values.name,
    description = new_values.description,
    phone_number = new_values.phone_number,
    location = new_values.location,
    starts_on = new_values.starts_on,
    ends_on = new_values.ends_on,
    starts_at = new_values.starts_at,
    ends_at = new_values.ends_at,
    frequency = new_values.frequency,
    separation = new_values.separation,
    recurrence_day = new_values.recurrence_day,
    recurrence_week = new_values.recurrence_week,
    recurrence_month = new_values.recurrence_month;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `Select_Temp_Event_Occurrence_Dates` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
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
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Current Database: `Tasks_Dev`
--

USE `Tasks_Dev`;

--
-- Final view structure for view `View_Checklist_Items`
--

/*!50001 DROP VIEW IF EXISTS `View_Checklist_Items`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `View_Checklist_Items` AS select `i`.`id` AS `id`,`r`.`id` AS `command_line_reference`,`i`.`checklist_id` AS `checklist_id`,`i`.`content` AS `content`,`i`.`position` AS `position`,`i`.`created_on` AS `created_on`,`i`.`completed_on` AS `completed_on` from (`Checklist_Items` `i` left join `Checklist_Items_Reference` `r` on((`r`.`checklist_item_id` = `i`.`id`))) group by `i`.`id` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Checklist_Labels`
--

/*!50001 DROP VIEW IF EXISTS `View_Checklist_Labels`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `View_Checklist_Labels` AS select `a`.`checklist_id` AS `checklist_id`,`c`.`user_id` AS `checklist_user_id`,`c`.`title` AS `checklist_title`,`c`.`checklist_type_id` AS `checklist_type_id`,`c`.`created_on` AS `checklist_created_on`,`a`.`label_id` AS `label_id`,`l`.`user_id` AS `label_user_id`,`l`.`name` AS `label_name`,`l`.`color` AS `label_color`,`l`.`created_on` AS `label_created_on`,`a`.`created_on` AS `created_on` from ((`Checklist_Labels` `a` left join `Checklists` `c` on((`c`.`id` = `a`.`checklist_id`))) left join `Labels` `l` on((`l`.`id` = `a`.`label_id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Checklists`
--

/*!50001 DROP VIEW IF EXISTS `View_Checklists`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `View_Checklists` AS select `c`.`id` AS `id`,`cr`.`id` AS `command_line_reference`,`c`.`user_id` AS `user_id`,`c`.`title` AS `title`,`c`.`checklist_type_id` AS `checklist_type_id`,`c`.`created_on` AS `created_on`,count(`i`.`id`) AS `count_items` from ((`Checklists` `c` left join `Checklist_Items` `i` on((`i`.`checklist_id` = `c`.`id`))) left join `Checklists_Reference` `cr` on((`cr`.`checklist_id` = `c`.`id`))) group by `c`.`id` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Events`
--

/*!50001 DROP VIEW IF EXISTS `View_Events`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `View_Events` AS select `e`.`id` AS `id`,`e`.`name` AS `name`,`e`.`description` AS `description`,`e`.`phone_number` AS `phone_number`,`e`.`location` AS `location`,`e`.`starts_on` AS `starts_on`,`e`.`ends_on` AS `ends_on`,`e`.`starts_at` AS `starts_at`,`e`.`ends_at` AS `ends_at`,`e`.`frequency` AS `frequency`,`e`.`separation` AS `separation`,`e`.`created_on` AS `created_on`,`e`.`recurrence_day` AS `recurrence_day`,`e`.`recurrence_week` AS `recurrence_week`,`e`.`recurrence_month` AS `recurrence_month` from `Events` `e` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Frequency_Counts`
--

/*!50001 DROP VIEW IF EXISTS `View_Frequency_Counts`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `View_Frequency_Counts` AS select (select count(0) from `Events` where (`Events`.`frequency` = 'ONCE')) AS `count_ONCE`,(select count(0) from `Events` where (`Events`.`frequency` = 'DAILY')) AS `count_DAILY`,(select count(0) from `Events` where (`Events`.`frequency` = 'WEEKLY')) AS `count_WEEKLY`,(select count(0) from `Events` where (`Events`.`frequency` = 'MONTHLY')) AS `count_MONTHLY`,(select count(0) from `Events` where (`Events`.`frequency` = 'YEARLY')) AS `count_YEARLY` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Labels`
--

/*!50001 DROP VIEW IF EXISTS `View_Labels`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `View_Labels` AS select `l`.`id` AS `id`,`l`.`user_id` AS `user_id`,`l`.`name` AS `name`,`l`.`color` AS `color`,`l`.`created_on` AS `created_on` from `Labels` `l` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `View_Users`
--

/*!50001 DROP VIEW IF EXISTS `View_Users`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`main`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `View_Users` AS select `u`.`id` AS `id`,`u`.`email` AS `email`,`u`.`password` AS `password`,`u`.`created_on` AS `created_on`,`u`.`deliver_reminders` AS `deliver_reminders`,(select `v`.`created_on` from `User_Email_Verifications` `v` where ((`v`.`user_id` = `u`.`id`) and (`v`.`email` = `u`.`email`) and (`v`.`confirmed_on` is not null)) limit 1) AS `email_confirmed_on` from `Users` `u` group by `u`.`id` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-10-18  9:34:33
-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: 104.225.208.163    Database: Tasks_Dev
-- ------------------------------------------------------
-- Server version	8.0.28-0ubuntu0.20.04.3

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Dumping data for table `Event_Frequencies`
--
-- ORDER BY:  `id`

LOCK TABLES `Event_Frequencies` WRITE;
/*!40000 ALTER TABLE `Event_Frequencies` DISABLE KEYS */;
REPLACE INTO `Event_Frequencies` VALUES (1,'once'),(2,'daily'),(3,'weekly'),(4,'monthly'),(5,'yearly');
/*!40000 ALTER TABLE `Event_Frequencies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Event_Action_Types`
--
-- ORDER BY:  `id`

LOCK TABLES `Event_Action_Types` WRITE;
/*!40000 ALTER TABLE `Event_Action_Types` DISABLE KEYS */;
REPLACE INTO `Event_Action_Types` VALUES (1,'completion'),(2,'cancellation');
/*!40000 ALTER TABLE `Event_Action_Types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `Checklist_Types`
--
-- ORDER BY:  `id`

LOCK TABLES `Checklist_Types` WRITE;
/*!40000 ALTER TABLE `Checklist_Types` DISABLE KEYS */;
REPLACE INTO `Checklist_Types` VALUES (1,'Checklist'),(2,'Template');
/*!40000 ALTER TABLE `Checklist_Types` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-10-18  9:34:37
