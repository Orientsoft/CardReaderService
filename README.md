# CardReaderService
Windows service for bridging serial cardreader (and printer, in the future) operations bi-directionally to HTTP endpoint.
# App.config
CardReaderService doesn't provide user adjustable settings. But at compiling-time, some features could be tuned by software provider.
```
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5.2" />
    </startup>
  <appSettings>
        <add key="LogSource" value="CardReaderService" />
        <add key="JsonpCallbackName" value="callback" />
        <add key="HttpEndpoint" value="http://localhost:29527/cardreader/" />
  </appSettings>
</configuration>
```
# Log
Check windows event viewer for log, all logs are exported to application category.
# Installation
Install service with InstallUtil. Start VS2015 x64 Native Tools cmdline with admin privilege.
```
installutil CardReaderService.exe
net start CardReaderService
net stop CardReaderService
installutil /u CardReaderService.exe
```
