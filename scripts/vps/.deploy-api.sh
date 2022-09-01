
printf "\n\n\n"
echo "------------------------------------------------"
echo "Compiling Tasks api project..."
echo "------------------------------------------------"
printf "\n\n\n"

# Compile Tasks project
cd /var/www/DotNetTasks/src/Tasks
dotnet publish Tasks -r linux-x64 --self-contained false -c Release


printf "\n\n\n"
echo "------------------------------------------------"
echo "Compiling Tasks.Reminders project..."
echo "------------------------------------------------"
printf "\n\n\n"

# compile Tasks.Reminders project
cd /var/www/DotNetTasks/src/Tasks
dotnet publish Tasks.Reminders -r linux-x64 --self-contained false -c Release


printf "\n\n\n"
echo "------------------------------------------------"
echo "Reloading the API linux service..."
echo "------------------------------------------------"
printf "\n\n\n"

# reload the service
cd /etc/systemd/system
systemctl daemon-reload
systemctl reload-or-restart DotNetTasksApi.service