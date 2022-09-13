OUTPUT_FILE=schemas.sql

# Dump the database schema
mysqldump Tasks_Dev Event_Frequencies Event_Action_Types
--user=main \
--column-statistics=FALSE \
--no-data \
--password \
--skip-comments \
--replace   \
--no-create-info \
--no-create-db