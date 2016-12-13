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
Source: "CardReaderService.exe.config"; DestDir: "{app}"
Source: "CardReaderService.pdb"; DestDir: "{app}"
Source: "1608card.dll"; DestDir: "{app}"
Source: "Mwic_32.dll"; DestDir: "{app}"
Source: "ViewShineICGas.dll"; DestDir: "{app}"
Source: "WDCRWV.DLL"; DestDir: "{app}"
Source: "WRwCard.dll"; DestDir: "{app}"
Source: "ZJWXGas.dll"; DestDir: "{app}"

[Run]
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "{app}\CardReaderService.exe"
Filename: "net"; Parameters: "start CardReaderService"

[UninstallRun]
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "/u {app}\CardReaderService.exe"