rmdir /s /q build

dotnet publish WarriorSurvivor/WarriorSurvivor.csproj -c Release --self-contained -r win-x64 /p:PublishSingleFile=true -o build/win/WarriorSurvivor
dotnet publish WarriorSurvivor/WarriorSurvivor.csproj -c Release --self-contained -r linux-x64 /p:PublishSingleFile=true -o build/linux/WarriorSurvivor