OUTPUT_FILE=enums.sql

# Dump the database schema
mysqldump Tasks_Dev Event_Frequencies Event_Action_Types \
--user=main \
--column-statistics=FALSE \
--password \
--skip-comments \
--replace   \
--no-create-info \
--no-create-db \
--result-file $OUTPUT_FILE

# change all occurence of Tasks_Dev to Tasks in the output file
./replace.sh $OUTPUT_FILE

# Copy over the data:
mysql -u main -p < $OUTPUT_FILE