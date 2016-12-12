[Setup]
AppName=CardReaderService
AppVersion=1.0
DefaultDirName={pf}\CardReaderService
DefaultGroupName=CardReaderService
Compression=lzma2
SolidCompression=yes
SourceDir=..\CardReaderService\bin\Release
OutputDir=..\..\..\CardReaderServiceInstaller\Output
OutputBaseFilename=CardReaderService_Installer

[Files]
Source: "CardReaderService.exe"; DestDir: "{app}"

[Run]
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "{app}\CardReaderService.exe"
Filename: "net"; Parameters: "start CardReaderService"

[UninstallRun]
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "/u {app}\CardReaderService.exe"