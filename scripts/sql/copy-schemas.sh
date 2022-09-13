# Dump the database:
mysqldump --user=main --column-statistics=FALSE --routines --events --no-data --password --skip-comments  --databases Tasks_Dev -r result.sql --add-drop-table

# Change the out document to be use Tasks instead of Tasks_Dev
sed -i 's/Tasks_Dev/Tasks/g' result.sql

# Copy over the data:
mysql -u main -p < result.sql



