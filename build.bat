rmdir /s /q build

dotnet publish WarriorSurvivor/WarriorSurvivor.csproj -c Release -r win-x64 /p:PublishSingleFile=true -o build/WarriorSurvivor