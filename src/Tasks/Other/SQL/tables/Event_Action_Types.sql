CREATE TABLE Event_Action_Types
(
    id SMALLINT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
    name CHAR(30) NOT NULL UNIQUE,
    PRIMARY KEY (id)
);

INSERT INTO Event_Action_Types 
    (name)
VALUES
    ('completion'),
    ('cancelation');