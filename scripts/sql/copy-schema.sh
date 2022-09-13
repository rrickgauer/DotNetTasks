
# https://dev.mysql.com/doc/refman/5.7/en/mysqldump.html

# Dump the database schema
mysqldump --databases Tasks_Dev --user=main --column-statistics=FALSE --routines --events --no-data --password --skip-comments -r result.sql --add-drop-table --triggers

# Change the out document to be use Tasks instead of Tasks_Dev
sed -i 's/Tasks_Dev/Tasks/g' result.sql

# Copy over the data:
mysql -u main -p < result.sql



