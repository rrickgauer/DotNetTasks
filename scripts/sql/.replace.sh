echo "Replacing all occurrences of Tasks_Dev to Tasks in $1"
sed -i 's/Tasks_Dev/Tasks/g' $1
