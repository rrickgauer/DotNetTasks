# Fetch the latest code from the main branch
echo ''
echo 'Fetching the latest code from GitHub...'
echo ''
cd /var/www/DotNetTasks
git pull

cd /var/www/DotNetTasks/scripts/vps

./deploy-api.sh
./deploy-gui.sh