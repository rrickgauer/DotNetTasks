#!/bin/bash

# Install any new python dependencies
cd /var/www/DotNetTasks/src/Gui
/var/www/DotNetTasks/src/Gui/.venv/bin/pip install -r requirements.txt

# Compile sass
cd /var/www/DotNetTasks/src/Gui/tasks/static/css
sass --quiet --no-source-map custom/style.scss dist/custom/style.css
sass --quiet --no-source-map custom/custom-bootstrap.scss dist/custom/bootstrap.css

# Compile js
cd /var/www/DotNetTasks/src/Gui/tasks/static/js
rollup -c rollup.config.js

# startup the script
cd /var/www/DotNetTasks/scripts/vps
./start-gui.sh
