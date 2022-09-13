
# https://dev.mysql.com/doc/refman/5.7/en/mysqldump.html

OUTPUT_FILE=schemas.sql

# Dump the database schema
mysqldump \
--databases Tasks_Dev \
--user=main \
--column-statistics=FALSE \
--routines \
--events \
--triggers\
--no-data \
--password \
--skip-comments \
--add-drop-table \
--result-file $OUTPUT_FILE


# Change the out document to be use Tasks instead of Tasks_Dev
#sed -i 's/Tasks_Dev/Tasks/g' result.sql
./replace.sh $OUTPUT_FILE

# Copy over the data:
mysql -u main -p < $OUTPUT_FILE



