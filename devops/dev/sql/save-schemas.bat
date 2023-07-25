mysqldump -u main -h 104.225.208.163 -p ^
--databases Tasks_Dev ^
--column-statistics=FALSE ^
--routines ^
--events ^
--add-drop-table ^
--allow-keywords ^
--no-create-db ^
--no-data ^
--result-file "C:\xampp\htdocs\files\DotNetTasks\sql\schema\.schemas.sql"


mysqldump -u main -h 104.225.208.163 -p ^
--column-statistics=FALSE ^
--allow-keywords ^
--no-create-db ^
--no-create-info ^
--replace ^
--order-by-primary ^
--result-file "C:\xampp\htdocs\files\DotNetTasks\sql\schema\.data.sql" ^
Tasks_Dev Event_Frequencies Event_Action_Types Checklist_Types

@echo off
cd "C:\xampp\htdocs\files\DotNetTasks\sql\schema"

type ".schemas.sql" ".data.sql" > "dump.sql"

del ".schemas.sql" /Q
del ".data.sql" /Q

pause
