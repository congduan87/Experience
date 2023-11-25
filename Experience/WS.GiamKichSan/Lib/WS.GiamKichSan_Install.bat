@echo off
SET INSTALL_UTIL_HOME=%WINDIR%"\Microsoft.NET\Framework\v4.0.30319"
SET SERVICE_HOME=%~dp0
SET SERVICE_EXE="WS.GiamKichSan.exe"
SET SERVICE_NAME="WS.GiamKichSan"

SET PATH=%PATH%;%INSTALL_UTIL_HOME%

cd /d %SERVICE_HOME%

echo Installing Service...
installutil %SERVICE_EXE%

echo Start Service
sc start %SERVICE_NAME%

Pause