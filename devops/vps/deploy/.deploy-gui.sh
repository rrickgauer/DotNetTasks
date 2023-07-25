#!/bin/bash


printf "\n\n\n"
echo "------------------------------------------------"
echo "Installing any new python dependencies..."
echo "------------------------------------------------"
printf "\n\n\n"

# Install any new python dependencies
cd /var/www/DotNetTasks/src/Gui
/var/www/DotNetTasks/src/Gui/.venv/bin/pip install -r requirements.txt


printf "\n\n\n"
echo "------------------------------------------------"
echo "Compiling CSS files..."
echo "------------------------------------------------"
printf "\n\n\n"

# Compile sass
cd /var/www/DotNetTasks/src/Gui/tasks/static/css
sass --quiet --no-source-map custom/style.scss dist/custom/style.css
sass --quiet --no-source-map custom/custom-bootstrap.scss dist/custom/bootstrap.css


printf "\n\n\n"
echo "------------------------------------------------"
echo "Compiling JavaScript files..."
echo "------------------------------------------------"
printf "\n\n\n"

# Compile js
cd /var/www/DotNetTasks/src/Gui/tasks/static/js
rollup -c rollup.config.js

# startup the script
cd /var/www/DotNetTasks/scripts/vps
./start-gui.sh
