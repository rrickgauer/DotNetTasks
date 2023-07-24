/***************************************************************

https://mariadb.com/kb/en/mariadb-error-codes/#shared-mariadbmysql-error-codes

****************************************************************/

namespace Tasks.Service.Domain.Constants;

public class MySqlErrorCodes
{
    public const int ER_HASHCHK                    = 1000;     // hashchk
    public const int ER_NISAMCHK                   = 1001;     // isamchk
    public const int ER_NO                         = 1002;     // NO
    public const int ER_YES                        = 1003;     // YES
    public const int ER_CANT_CREATE_FILE           = 1004;     // Can't create file 's' (errno: d)
    public const int ER_CANT_CREATE_TABLE          = 1005;     // Can't create table 's' (errno: d)
    public const int ER_CANT_CREATE_DB             = 1006;     // Can't create database 's' (errno: d
    public const int ER_DB_CREATE_EXISTS           = 1007;     // Can't create database 's'; database exists
    public const int ER_DB_DROP_EXISTS             = 1008;     // Can't drop database 's'; database doesn't exist
    public const int ER_DB_DROP_DELETE             = 1009;     // Error dropping database (can't delete 's', errno: d)
    public const int ER_DB_DROP_RMDIR              = 1010;     // Error dropping database (can't rmdir 's', errno: d)
    public const int ER_CANT_DELETE_FILE           = 1011;     // Error on delete of 's' (errno: d)
    public const int ER_CANT_FIND_SYSTEM_REC       = 1012;     // Can't read record in system table
    public const int ER_CANT_GET_STAT              = 1013;     // Can't get status of 's' (errno: d)
    public const int ER_CANT_GET_WD                = 1014;     // Can't get working directory (errno: d)
    public const int ER_CANT_LOCK                  = 1015;     // Can't lock file (errno: d)
    public const int ER_CANT_OPEN_FILE             = 1016;     // Can't open file: 's' (errno: d)
    public const int ER_FILE_NOT_FOUND             = 1017;     // Can't find file: 's' (errno: d)
    public const int ER_CANT_READ_DIR              = 1018;     // Can't read dir of 's' (errno: d)
    public const int ER_CANT_SET_WD                = 1019;     // Can't change dir to 's' (errno: d)
    public const int ER_CHECKREAD                  = 1020;     // Record has changed since last read in table 's'
    public const int ER_DISK_FULL                  = 1021;     // Disk full (s); waiting for someone to free some space...
    public const int ER_DUP_KEY                    = 1022;     // Can't write; duplicate key in table 's'
    public const int ER_ERROR_ON_CLOSE             = 1023;     // Error on close of 's' (errno: d)
    public const int ER_ERROR_ON_READ              = 1024;     // Error reading file 's' (errno: d)
    public const int ER_ERROR_ON_RENAME            = 1025;     // Error on rename of 's' to 's' (errno: d)
    public const int ER_ERROR_ON_WRITE             = 1026;     // Error writing file 's' (errno: d)
    public const int ER_FILE_USED                  = 1027;     // is locked against change
    public const int ER_FILSORT_ABORT              = 1028;     // Sort aborted
    public const int ER_FORM_NOT_FOUND             = 1029;     // View 's' doesn't exist for 's'
    public const int ER_GET_ERRN                   = 1030;     // Got error d from storage engine
    public const int ER_ILLEGAL_HA                 = 1031;     // Table storage engine for 's' doesn't have this option
    public const int ER_KEY_NOT_FOUND              = 1032;     // Can't find record in 's'
    public const int ER_NOT_FORM_FILE              = 1033;     // Incorrect information in file: 's'
    public const int ER_NOT_KEYFILE                = 1034;     // Incorrect key file for table 's'; try to repair it
    public const int ER_OLD_KEYFILE                = 1035;     // Old key file for table 's'; repair it!
    public const int ER_OPEN_AS_READONLY           = 1036;     // Table 's' is read only
    public const int ER_OUTOFMEMORY                = 1037;     // Out of memory; restart server and try again (needed d bytes)
    public const int ER_OUT_OF_SORTMEMORY          = 1038;     // Out of sort memory, consider increasing server sort buffer size
    public const int ER_UNEXPECTED_EOF             = 1039;     // Unexpected EOF found when reading file 's' (Errno: d)
    public const int ER_CON_COUNT_ERROR            = 1040;     // Too many connections
    public const int ER_OUT_OF_RESOURCES           = 1041;     // Out of memory; check if mysqld or some other process uses all available memory; if not, you may have to use 'ulimit' to allow mysqld to use more memory or you can add more swap space
    public const int ER_BAD_HOST_ERROR             = 1042;     // Can't get hostname for your address
    public const int ER_HANDSHAKE_ERROR            = 1043;     // Bad handshake
    public const int ER_DBACCESS_DENIED_ERROR      = 1044;     // Access denied for user 's'@'s' to database 's'
    public const int ER_ACCESS_DENIED_ERROR        = 1045;     // Access denied for user 's'@'s' (using password: s)
    public const int ER_NO_DB_ERROR                = 1046;     // No database selected
    public const int ER_UNKNOWN_COM_ERROR          = 1047;     // Unknown command
    public const int ER_BAD_NULL_ERROR             = 1048;     // Column 's' cannot be null
    public const int ER_BAD_DB_ERROR               = 1049;     // Unknown database 's'
    public const int ER_TABLE_EXISTS_ERROR         = 1050;     // Table 's' already exists
    public const int ER_BAD_TABLE_ERROR            = 1051;     // Unknown table 's'
    public const int ER_NON_UNIQ_ERROR             = 1052;     // Column 's' in s is ambiguous
    public const int ER_SERVER_SHUTDOWN            = 1053;     // Server shutdown in progress
    public const int ER_BAD_FIELD_ERROR            = 1054;     // Unknown column 's' in 's'
    public const int ER_WRONG_FIELD_WITH_GROUP     = 1055;     // isn't in GROUP BY
    public const int ER_WRONG_GROUP_FIELD          = 1056;     // Can't group on 's'
    public const int ER_WRONG_SUM_SELECT           = 1057;     // Statement has sum functions and columns in same statement
    public const int ER_WRONG_VALUE_COUNT          = 1058;     // Column count doesn't match value count
    public const int ER_TOO_LONG_IDENT             = 1059;     // Identifier name 's' is too long
    public const int ER_DUP_FIELDNAME              = 1060;     // Duplicate column name 's'
    public const int ER_DUP_KEYNAME                = 1061;     // Duplicate key name 's'
    public const int ER_DUP_ENTRY                  = 1062;     // Duplicate entry 's' for key d
    public const int ER_WRONG_FIELD_SPEC           = 1063;     // Incorrect column specifier for column 's'
    public const int ER_PARSE_ERROR                = 1064;     // s near 's' at line d
    public const int ER_EMPTY_QUERY                = 1065;     // Query was empty
    public const int ER_NONUNIQ_TABLE              = 1066;     // Not unique table/alias: 's'
    public const int ER_INVALID_DEFAULT            = 1067;     // Invalid default value for 's'
    public const int ER_MULTIPLE_PRI_KEY           = 1068;     // Multiple primary key defined
    public const int ER_TOO_MANY_KEYS              = 1069;     // Too many keys specified; max d keys allowed
    public const int ER_TOO_MANY_KEY_PARTS         = 1070;     // Too many key parts specified; max d parts allowed
    public const int ER_TOO_LONG_KEY               = 1071;     // Specified key was too long; max key length is d bytes
    public const int ER_KEY_COLUMN_DOES_NOT_EXITS  = 1072;     // Key column 's' doesn't exist in table
    public const int ER_BLOB_USED_AS_KEY           = 1073;     // BLOB column 's' can't be used in key specification with the used table type
    public const int ER_TOO_BIG_FIELDLENGTH        = 1074;     // Column length too big for column 's' (max = lu); use BLOB or TEXT instead
    public const int ER_WRONG_AUTO_KEY             = 1075;     // Incorrect table definition; there can be only one auto column and it must be defined as a key
    public const int ER_READY                      = 1076;     // ready for connections. Version: 's' socket: 's' port: d
    public const int ER_NORMAL_SHUTDOWN            = 1077;     // Normal shutdown
    public const int ER_GOT_SIGNAL                 = 1078;     // Got signal d. Aborting!
    public const int ER_SHUTDOWN_COMPLETE          = 1079;     // Shutdown complete
    public const int ER_FORCING_CLOSE              = 1080;     // Forcing close of thread ld user: 's'
    public const int ER_IPSOCK_ERROR               = 1081;     // Can't create IP socket
    public const int ER_NO_SUCH_INDEX              = 1082;     // Table 's' has no index like the one used in CREATE INDEX; recreate the table
    public const int ER_WRONG_FIELD_TERMINATORS    = 1083;     // Field separator argument is not what is expected; check the manual
    public const int ER_BLOBS_AND_NO_TERMINATED    = 1084;     // You can't use fixed rowlength with BLOBs; please use 'fields terminated by'
    public const int ER_TEXTFILE_NOT_READABLE      = 1085;     // The file 's' must be in the database directory or be readable by all
    public const int ER_FILE_EXISTS_ERROR          = 1086;     // File 's' already exists
    public const int ER_LOAD_INF                   = 1087;     // Records: ld Deleted: ld Skipped: ld Warnings: ld
    public const int ER_ALTER_INF                  = 1088;     // Records: ld Duplicates: ld
    public const int ER_WRONG_SUB_KEY              = 1089;     // Incorrect prefix key; the used key part isn't a string, the used length is longer than the key part, or the storage engine doesn't support unique prefix keys
    public const int ER_CANT_REMOVE_ALL_FIELDS     = 1090;     // You can't delete all columns with ALTER TABLE; use DROP TABLE instead
    public const int ER_CANT_DROP_FIELD_OR_KEY     = 1091;     // Can't DROP 's'; check that column/key exists
    public const int ER_INSERT_INF                 = 1092;     // Records: ld Duplicates: ld Warnings: ld
    public const int ER_UPDATE_TABLE_USED          = 1093;     // You can't specify target table 's' for update in FROM clause
    public const int ER_NO_SUCH_THREAD             = 1094;     // Unknown thread id: lu
    public const int ER_KILL_DENIED_ERROR          = 1095;     // You are not owner of thread lu
    public const int ER_NO_TABLES_USED             = 1096;     // No tables used
    public const int ER_TOO_BIG_SET                = 1097;     // Too many strings for column s and SET
    public const int ER_NO_UNIQUE_LOGFILE          = 1098;     // Can't generate a unique log-filename s.(1-999)
    public const int ER_TABLE_NOT_LOCKED_FOR_WRITE = 1099;     // Table 's' was locked with a READ lock and can't be updated
}
