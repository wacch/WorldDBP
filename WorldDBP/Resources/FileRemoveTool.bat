@echo off
echo %1
echo %2
echo %3

cd /d %2
for /f "skip=%1" %%F in ('dir /b /o-d *.zip') do del %%F
REM for /f "skip=%1" %%F in ('dir /b /o-d %3*') do echo %%F
timeout 5