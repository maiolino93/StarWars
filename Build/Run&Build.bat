cd ..
dotnet build
cd .\Kneat.Service
start Start.bat
timeout /t 5
cd ..
cd .\Kneat.UI
start Start.bat
