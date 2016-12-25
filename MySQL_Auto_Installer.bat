@echo off
echo Installing MySQL Server. Please wait...

msiexec /i "mysql-installer-community-5.6.14.0.msi" /qn
if errorlevel 1 echo Unsuccessful

echo Configurating MySQL Server...
cd "C:\Program Files\MySQL\MySQL Server 5.6\bin\" <-- set folder first, then run executeable
mysqlinstanceconfig.exe
-i -q ServiceName=MySQL RootPassword=mysql ServerType=DEVELOPER 
DatabaseType=MYISAM Port=3306 Charset=utf8

echo Installation was successfully