#!/bin/bash

IP_ADDRESS='104.225.208.163'

echo 'Starting up front-end TESTING server...'

cd /var/www/DotNetTasks/src/Gui

mod_wsgi-express start-server \
--user www-data  \
--group www-data  \
--server-name tasks.ryanrickgauer.com  \
--port 5031   \
--access-log  \
--log-level info   \
--host $IP_ADDRESS \
--log-to-terminal \
tasks.wsgi



