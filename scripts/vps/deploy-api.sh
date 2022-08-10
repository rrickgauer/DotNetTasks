
# Fetch the latest code from the main branch
echo ''
echo 'Fetching the latest code from GitHub...'
echo ''
cd /var/www/DotNetTasks
git pull

# Compile the code
echo ''
echo ''
echo 'Compiling the code...'
echo ''
cd /var/www/DotNetTasks/src/Tasks
dotnet publish Tasks -r linux-x64 --self-contained false -c Release

# reload the service
echo ''
echo ''
echo 'Reloading the service...'
echo ''
cd /etc/systemd/system
systemctl daemon-reload
systemctl reload-or-restart DotNetTasksApi.service