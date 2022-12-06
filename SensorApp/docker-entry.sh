#!/usr/bin/env bash

sed -i "s/db_ConnectionString/$DB_CONNECTIONSTRING/" /app/appsettings.json
sed -i "s/db_nameDataBase/$DB_NAMEDATEBASE/" /app/appsettings.json


sed -i "s/rb_HostName/$RB_HOSTNAME/" /app/appsettings.json
sed -i "s/rb_Port/$RB_PORT/" /app/appsettings.json
sed -i "s/rb_UserName/$RB_USERNAME/" /app/appsettings.json
sed -i "s/rb_Password/$RB_PASSSWORD/" /app/appsettings.json
sed -i "s/rb_Queue/$RB_QUEUE/" /app/appsettings.json

dotnet SensorApp.API.dll