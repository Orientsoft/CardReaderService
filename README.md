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
# Manual Installation
Install service with InstallUtil. Start VS2015 x64 Native Tools cmdline with admin privilege.
```
installutil CardReaderService.exe
net start CardReaderService
net stop CardReaderService
installutil /u CardReaderService.exe
```
# Automatic Installation
An Inno Setup script has been created under CardReaderService/CardReaderServiceInstaller.  
Compiling the script will create an installer under CardReaderService/CardReaderServiceInstaller/Output.  
The installer will copy all files needed and start the service automatically. When uninstalling, the service will be stopped and removed.  
CardReaderService will start automatically during system reboot.
# API  
By default, CardReaderService response to JSONP request from http://localhost:29527/cardreader endpoint.  
* Common Parameter  
These parameters are required by every API of CardReaderService.  
   1. type - 0 (card reader), 1 (printer, not implemented yet)  
   2. vendor - ZJWX  
   3. operation - for ZJWX, checkreader, makecard, clearcard, writecard, readcard  
* ZJWX Operation  
For ZJWX card reader, parameters are depends on operation.  
   1. checkreader  
      No parameter needed.
   2. makecard  
      to be filled
   3. clearcard  
      to be filled  
   4. writecard  
      PamaInfo - 表类型|卡类型|卡号|充值量|充值序号|表存上限|透支量|报警量|闲置时间 (eg. 1|5|012345678909|10.00|FFFF|100.0|0.0|0|100)  
      LadderInfo - 单价序号|价格执行时间(yyMMddHH) |阶梯周期起始时间(YYMMDD)|阶梯1价格| 阶梯1气量|阶梯2价格| 阶梯2气量|阶梯3价格|阶梯3气量|阶梯4价格|阶梯4气量|阶梯5价格|阶梯5气量 (eg. |||1.0000|||||||||)  
   5. readcard
      No parameter needed.
* Return value
CardReaderService will return standard HTTP status code, as well as JSONP response object.  
* Example  
For example, the following JSONP call should write info to card:  
```
http://localhost:29527/cardreader?type=0&vendor=ZJWX&operation=writecard&callback=jsonpcb&PamaInfo=1|5|012345678909|10.00|FFFF|100.0|0.0|0|100&LadderInfo=|||1.0000|||||||||  
```
If the operation is successful, you will receive 200 status code and the following content:  
```
jsonpcb({"write":"OK"})  
```
Otherwise, error status code and error content will be returned.  
