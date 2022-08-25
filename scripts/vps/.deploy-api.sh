
printf "\n\n\n"
echo "------------------------------------------------"
echo "Compiling DotNet solution..."
echo "------------------------------------------------"
printf "\n\n\n"

# Compile the code
cd /var/www/DotNetTasks/src/Tasks
dotnet publish Tasks -r linux-x64 --self-contained false -c Release

printf "\n\n\n"
echo "------------------------------------------------"
echo "Reloading the API linux service..."
echo "------------------------------------------------"
printf "\n\n\n"

# reload the service
cd /etc/systemd/system
systemctl daemon-reload
systemctl reload-or-restart DotNetTasksApi.service