#!/bin/bash

IP_ADDRESS='104.225.208.163'

#---------------------------------------
# Start up the API
#---------------------------------------
echo 'Starting up gui server...'

cd /var/www/DotNetTasks/src/Gui

mod_wsgi-express setup-server \
--user www-data  \
--group www-data  \
--server-name tasks.ryanrickgauer.com  \
--port 5021   \
--access-log  \
--log-level info   \
--server-root /etc/tasks.ryanrickgauer.com \
--host $IP_ADDRESS \
--setup-only \
tasks.wsgi

# restart the server
/etc/tasks.ryanrickgauer.com/apachectl restart