[Setup]
AppName=CardReaderService
AppVersion=1.0
DefaultDirName=C:\CardReaderService
DefaultGroupName=CardReaderService
Compression=lzma2
SolidCompression=yes
SourceDir=..\CardReaderService\bin\Release
OutputDir=..\..\..\CardReaderServiceInstaller\Output
OutputBaseFilename=CardReaderService_Installer

[Files]
Source: "CardReaderService.exe"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "CardReaderService.exe.config"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "CardReaderService.pdb"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "1608card.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "Mwic_32.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "ViewShineICGas.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "WDCRWV.DLL"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "WRwCard.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "ZJWXGas.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion

[Run]
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "{app}\CardReaderService.exe"
Filename: "net"; Parameters: "start CardReaderService"

[UninstallRun]
Filename: "net"; Parameters: "stop CardReaderService"
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "/u {app}\CardReaderService.exe"