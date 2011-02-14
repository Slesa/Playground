@echo off
cls
call "tools\VisualStudio\vcvarsall.bat" x86 

:Build
"tools\FAKE\Fake.exe" build.fsx

rem Bail if we're running a TeamCity build.
if defined TEAMCITY_PROJECT_NAME goto Quit

rem Loop the build script.
set CHOICE=nothing
echo (Q)uit, or (Enter) runs the build again
set /P CHOICE= 
if /i "%CHOICE%"=="Q" goto :Quit

GOTO Build

:Quit
exit /b %errorlevel%

