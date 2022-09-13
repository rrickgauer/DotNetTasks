# https://dev.mysql.com/doc/refman/8.0/en/mysqldump.html

mysqldump   \
--databases Tasks \
--user=main     \
--column-statistics=FALSE   \
--password  \
--skip-comments \
--replace   \
--no-create-info \
--no-create-db \
--result-file tasks-data-dump.sql 