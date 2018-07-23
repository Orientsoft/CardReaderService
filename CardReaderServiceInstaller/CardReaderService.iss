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
Source: "CardReaderService.exe"; DestDir: "{app}"; Flags: ignoreversion replacesameversion restartreplace
Source: "CardReaderService.exe.config"; DestDir: "{app}"; Flags: ignoreversion replacesameversion restartreplace

Source: "borlndmm.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "gsIneterface.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "h_cmi.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "l_cmi.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "opends60.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "setupapi.dll"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "sysaram.ini"; DestDir: "{app}"; Flags: ignoreversion replacesameversion

Source: "Mscomm32.ocx"; DestDir: "{sys}"; Flags: restartreplace regserver

[Run]
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "{app}\CardReaderService.exe"
Filename: "net"; Parameters: "start CardReaderService"

[UninstallRun]
Filename: "net"; Parameters: "stop CardReaderService"
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "/u {app}\CardReaderService.exe"
Filename: "regsvr32"; Parameters: "/u Mscomm32.ocx"

[UninstallDelete]
Type: files; Name:"{sys}\Mscomm32.ocx"