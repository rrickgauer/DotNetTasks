#!/bin/bash

IP_ADDRESS='104.225.208.163'

#---------------------------------------
# Start up the API
#---------------------------------------

printf "\n\n\n"
echo "------------------------------------------------"
echo 'Starting up gui server...'
echo "------------------------------------------------"
printf "\n\n\n"

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


# restart the apache server

printf "\n\n\n"
echo "------------------------------------------------"
echo 'Restarting the Apache service...'
echo "------------------------------------------------"
printf "\n\n\n"

/etc/tasks.ryanrickgauer.com/apachectl restart