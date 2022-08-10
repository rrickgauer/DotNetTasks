# Install any new python dependencies
cd /var/www/DotNetTasks/src/Gui
/var/www/DotNetTasks/src/Gui/.venv/bin/pip install -r requirements.txt

# Compile sass
cd /var/www/DotNetTasks/src/Gui/tasks/static/css
sass custom/style.scss dist/custom/style.css

# Compile js
cd /var/www/DotNetTasks/src/Gui/tasks/static/js
rollup -c rollup.config.js

# startup the script
cd /var/www/DotNetTasks/scripts/vps
./start-gui.sh
