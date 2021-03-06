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
# .NET Framework  
.Net framework 4.0 or higher is required.  
# API  
By default, CardReaderService response to JSONP request from http://localhost:29527/cardreader endpoint.  
* Common Parameter  
These parameters are required by every API of CardReaderService.  
   1. type - 0 (card reader), 1 (printer, not implemented yet)  
   2. vendor - ZJWX  
   3. operation - for ZJWX, checkreader, makecard, clearcard, writecard, readcard  
* ZJWX Operation  
For ZJWX card reader, parameters depends on operation.  
   1. checkreader  
      No parameter needed.
   2. makecard  
      WatchType - 1:气量表, 2:金额表  
      CardType - 1:小表设置卡, 2:大表设置卡, 3:清除卡, 4:转存卡, 5:用户卡, 6:检测卡  
      CardNo - For CardType=1, CardNo must be 07FFFFFFFFFF, for CardType=2, CardNo must be 17FFFFFFFFFF  
      For CardType=5, CardNo must start with 0 (eg. 012345678909)  
      Otherwise, CardNo should be 4 digits.  
      Most card type could be set only once. So be careful with CardType setting.
   3. clearcard  
      No parameter needed.  
   4. writecard  
      PamaInfo - 表类型|卡类型|卡号|充值量|充值序号|表存上限|透支量|报警量|闲置时间 (eg. 1|5|012345678909|10.00|FFFF|100.0|0.0|0|99)  
      Please be aware that idle time should less than 100.  
      LadderInfo - 单价序号|价格执行时间(yyMMddHH) |阶梯周期起始时间(YYMMDD)|阶梯1价格| 阶梯1气量|阶梯2价格| 阶梯2气量|阶梯3价格|阶梯3气量|阶梯4价格|阶梯4气量|阶梯5价格|阶梯5气量 (eg. |||1.0000|||||||||)  
   5. readcard  
      No parameter needed. 
* Haili Operation  
For Haili card reader:  
   1. setreader  
      port - COM port of the reader, could always be 1.  
      baudrate - COM port baudrate, could always be 9600.  
   2. readcard  
      No parameter needed.  
   3. writecard  
      klx - 卡类型, could always be 80.  
      kh - 卡号, should be 8 digits.  
      ql - 购气量  
      cs - 购气次数  
      ljgql - 累计购气量  
   4. clearcard  
      No parameter needed.  
   5. makecard  
      klx - 卡类型, could always be 80.  
      kzt - 卡状态, set to 1.  
      kh - 卡号, should be 8 digits.  
      tm - 表条码号  
      ql - 购气量  
      cs - 购气次数  
      ljgql - 累计购气量  
      bkcs - 补卡次数  
      ljyql - 累计用气量, always set to 0.  
   6. checkreader - don't rely on this since it will success as long as there's a card in reader  
      No parameter needed.  
   7. clearwatch - make a card that could be used to clear the watch  
      klx - 卡类型.  
      kh - 卡号, should be 8 digits.  
* Return Value  
CardReaderService will return standard HTTP status code, as well as JSONP response object.  
* Flow    
   1. 总流程：清零卡->开户卡->用户卡（购气）  
   2. 补卡流程：可以直接制用户卡，补卡次数+1，注意原卡中未上表部分的处理  
   3. 开户卡流程：基本与用户卡一致，但是卡状态为0  
   4. 购气流程：读卡，验证ljgql（累计购气量）与syql（剩余气量）是否一致，若syql较小，说明卡内有气未上表，应该提醒客户先上表，再购气  
* Example  
For example, the following JSONP call should write info to card:  
```
http://localhost:29527/cardreader?type=0&vendor=ZJWX&operation=writecard&callback=jsonpcb&PamaInfo=1|5|012345678909|10.00|FFFF|100.0|0.0|0|99&LadderInfo=|||1.0000|||||||||  
```
If the operation is successful, you will receive 200 status code and the following content:  
```
jsonpcb({"write":"OK"})  
```
Otherwise, error status code and error content will be returned.  
