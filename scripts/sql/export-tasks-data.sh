# https://dev.mysql.com/doc/refman/8.0/en/mysqldump.html

mysqldump   \
--databases=Tasks_Dev   \
--user=main     \
--column-statistics=FALSE   \
--password  \
--skip-comments     \
--replace   \
--result-file=tasks-data-dump.sql 

