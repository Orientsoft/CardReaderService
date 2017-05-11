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
Source: "1608card.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "Mwic_32.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "ViewShineICGas.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "WDCRWV.DLL"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "WRwCard.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "ZJWXGas.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "HLICCard.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "HLICCSEC.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "PrinterAgent.exe"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "PrinterAgent.exe.config"; DestDir: "{app}"; Flags: ignoreversion replacesameversion

[Run]
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "{app}\CardReaderService.exe"
Filename: "net"; Parameters: "start CardReaderService"
Filename: "{app}\PrinterAgent.exe"

[UninstallRun]
Filename: "net"; Parameters: "stop CardReaderService"
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "/u {app}\CardReaderService.exe"
Filename: "taskkill"; Parameters: "/im PrinterAgent.exe /f"