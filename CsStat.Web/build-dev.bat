cls
Echo "Building assets"
call yarn
call yarn run dev:build

cls

Echo "Building solution/project file using batch file"
SET PATH=c:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin
SET SolutionPath=d:\projects\counterstrikestat\Latest\CounterStrikeStat.sln
Echo Start Time - %Time%
MSbuild %SolutionPath% /p:Configuration=Debug /p:Platform="Any CPU"
Echo End Time - %Time%
Set /p Wait=Build Process Completed...