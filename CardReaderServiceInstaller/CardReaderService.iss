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
Source: "start-printer.bat"; DestDir: "{app}"; Flags: ignoreversion replacesameversion
Source: "D:\software\Grid++Report 6\gregn6.dll"; DestDir: "{app}"; Flags: restartreplace sharedfile regserver
Source: "D:\software\Grid++Report 6\grdes6.dll"; DestDir: "{app}"; Flags: restartreplace sharedfile regserver
Source: "D:\software\openssl-1.0.2l-x64_86-win64\localhost.pfx"; DestDir: "{app}"; Flags: ignoreversion replacesameversion

[Icons]
Name: "{commonstartup}\PrinterAgent"; Filename: "{app}\PrinterAgent.exe"; WorkingDir: "{app}"

[Run]
Filename: "certutil"; Parameters: "-p welcome1 -importpfx ""TrustedPublisher"" {app}\localhost.pfx"; StatusMsg: "Adding trusted publisher..." 
Filename: "certutil"; Parameters: "-p welcome1 -importpfx ""My"" {app}\localhost.pfx"; StatusMsg: "Adding personal certification..."
Filename: "netsh"; Parameters: "http add sslcert"" ipport=0.0.0.0:29527"" certhash=4d5debaf01ffe65ec8c401635b69c7fd2c768dec"" appid={{29558ad6-b830-4379-9dca-568a96d16841}"" clientcertnegotiation=enable"
Filename: "netsh"; Parameters: "http add sslcert"" ipport=0.0.0.0:39527"" certhash=4d5debaf01ffe65ec8c401635b69c7fd2c768dec"" appid={{29558ad6-b830-4379-9dca-568a96d16841}"" clientcertnegotiation=enable"
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "{app}\CardReaderService.exe"
Filename: "net"; Parameters: "start CardReaderService"
Filename: "{app}\start-printer.bat"

[UninstallRun]
Filename: "net"; Parameters: "stop CardReaderService"
Filename: "{dotnet40}\InstallUtil.exe"; Parameters: "/u {app}\CardReaderService.exe"
Filename: "taskkill"; Parameters: "/im PrinterAgent.exe /f"
Filename: "certutil"; Parameters: "-delstore ""TrustedPublisher"" 8302bcd7beb18994"; StatusMsg: "Removing trusted publisher..."
Filename: "certutil"; Parameters: "-delstore ""My"" 8302bcd7beb18994"; StatusMsg: "Removing personal certification..."
Filename: "netsh"; Parameters: "http delete sslcert"" ipport=0.0.0.0:29527";
Filename: "netsh"; Parameters: "http delete sslcert"" ipport=0.0.0.0:39527";