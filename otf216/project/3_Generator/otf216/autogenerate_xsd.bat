@echo off
CLS

ECHO Preparations
for %%I in (.) do set CurrDirName=%%~nxI
ECHO Subfoldername = %CurrDirName%

ECHO.
ECHO Step (1/3) - Generate combined XSD file
set targetXSD_combined=%cd%\autogenerated_xsd_combined
if not exist "%targetXSD_combined%" mkdir "%targetXSD_combined%"
del %targetXSD_combined%\*.xsd

xsd.exe "%CurrDirName%.dll"
ren *.xsd "autogenerated_%CurrDirName%.xsd"
move *.xsd %targetXSD_combined%
copy *.dll %targetXSD_combined%

ECHO.
ECHO Step (2/3) - Generate specific XSD files
set targetXSD_separate=%cd%\autogenerated_xsd_separate
if not exist "%targetXSD_separate%" mkdir "%targetXSD_separate%"
del %targetXSD_separate%\*.xsd

call _gen.bat Message_Container_PreNotification, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_Container_PreNotification_StatusUpdate, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_Container_StatusUpdate_Storage_Arrival, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_Container_StatusUpdate_Storage_Available, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_Container_StatusUpdate_Storage_Departure, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_Container_StatusUpdate_Work, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_Container_Work_Estimate, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_InspectionReport, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_PreNotification, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_PreNotification_StatusUpdate, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_StatusUpdate_Cleaning, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_StatusUpdate_Heating, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_StatusUpdate_Inspection, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_StatusUpdate_Storage_Arrival, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_StatusUpdate_Storage_Available, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_StatusUpdate_Storage_Departure, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_StatusUpdate_Transhipment, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_StatusUpdate_Work, %CurrDirName%.dll, %targetXSD_separate%
call _gen.bat Message_TankContainer_Work_Estimate, %CurrDirName%.dll, %targetXSD_separate%

ECHO.
ECHO Step (3/3) - Generation Completed

PAUSE
