CREATE TABLE Event_Actions (
  event_id CHAR(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  on_date DATE NOT NULL,
  event_action_type_id SMALLINT UNSIGNED NOT NULL,
  created_on TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (event_id, on_date, event_action_type_id),
  FOREIGN KEY (event_id) REFERENCES Events(id) ON UPDATE CASCADE ON DELETE CASCADE,
  FOREIGN KEY (event_action_type_id) REFERENCES Event_Action_Types(id) ON UPDATE CASCADE
) ENGINE=INNODB;