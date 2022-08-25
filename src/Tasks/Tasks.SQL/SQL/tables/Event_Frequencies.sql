CREATE TABLE Event_Frequencies
(
    id TINYINT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
    name CHAR(30) NOT NULL UNIQUE,
    PRIMARY KEY (id)
);


INSERT IGNORE INTO Event_Frequencies 
    (name)
VALUES
    ('once'),
    ('daily'),
    ('weekly'),
    ('monthly'),
    ('yearly');
