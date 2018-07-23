using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

// http deps
using System.Net;
using System.IO;
using System.Threading;
using System.Configuration;

namespace CardReaderService
{
    public class ParameterParser
    {
        public static int parseInt(
            HttpListenerRequest request,
            string key,
            int defaultValue
        )
        {
            int _value = defaultValue;
            int.TryParse(request.QueryString[key], out _value);
            int value = _value;

            return value;
        }

        public static Int32 parseInt32(
            HttpListenerRequest request,
            string key,
            Int32 defaultValue
        )
        {
            Int32 _value = defaultValue;
            Int32.TryParse(request.QueryString[key], out _value);
            Int32 value = _value;

            return value;
        }

        public static short parseShort(
            HttpListenerRequest request,
            string key,
            short defaultValue
        )
        {
            short _value = defaultValue;
            short.TryParse(request.QueryString[key], out _value);
            short value = _value;

            return value;
        }

        public static float parseFloat(
            HttpListenerRequest request,
            string key,
            float defaultValue
        )
        {
            float _value = defaultValue;
            float.TryParse(request.QueryString[key], out _value);
            float value = _value;

            return value;
        }

        public static string convertByteArrayToString(byte[] source)
        {
            return Encoding.Default.GetString(source).Trim('\0');
        }

        public static byte[] convertStringToByteArray(string source)
        {
            return Encoding.Default.GetBytes(source);
        }
    }

    public class JsonpHandler
    {
        public static string handle(HttpListenerRequest req, string resp)
        {
            string callback = req.QueryString[ConfigurationManager.AppSettings["JsonpCallbackName"]];
            string script = callback + "(" + resp + ")";

            return script;
        }
    }

    public class Crypto
    {
        public static string keyseed = "9527AA55";
        public static string decode(string encodedString, string key)
        {
            encodedString = encodedString.Replace('-', '+');
            encodedString = encodedString.Replace('*', '/');
            encodedString = encodedString.Replace('^', '=');

            byte[] keyBytes = Encoding.Unicode.GetBytes(key);
            byte[] encodedBytes = Convert.FromBase64String(encodedString.Trim('\0'));
            for (int i = 0; i < encodedBytes.Length; i += 2)
            {
                for (int j = 0; j < keyBytes.Length; j += 2)
                {
                    encodedBytes[i] = Convert.ToByte(encodedBytes[i] ^ keyBytes[j]);
                }
            }
            string decodedString = Encoding.Unicode.GetString(encodedBytes).TrimEnd('\0');
            return decodedString;
        }

        public static string encode(string plainString, string key)
        {
            byte[] keyBytes = Encoding.Unicode.GetBytes(key);
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainString);

            for (int i = 0; i < plainBytes.Length; i += 2)
            {
                for (int j = 0; j < keyBytes.Length; j += 2)
                {
                    plainBytes[i] = Convert.ToByte(plainBytes[i] ^ keyBytes[j]);
                }
            }

            string encodedString = Convert.ToBase64String(plainBytes);
            encodedString.Replace('+', '-');
            encodedString.Replace('/', '*');
            encodedString.Replace('=', '^');

            return encodedString;
        }
    }

    public class ListenerThread
    {
        private bool listenFlag;
        private HttpListener listener;

        public ListenerThread()
        {
            listenFlag = false;
            listener = new HttpListener();
        }

        ~ListenerThread()
        {
            listener.Close();
        }

        public void start()
        {
            // init cardreader
            WaterCardReader waterCardReader = new WaterCardReader();

            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            try
            {
                listener.Prefixes.Add(ConfigurationManager.AppSettings["HttpEndpoint"]);
                listener.Start();
            }
            catch (Exception e)
            {
                EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], e.Message + "\r\n" + e.StackTrace, EventLogEntryType.Error);
            }
            
            listenFlag = true;
            EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "HTTP listener started.", EventLogEntryType.Information);

            while (listenFlag)
            {
                string jsonp = null;

                HttpListenerContext ctx;
                try
                {
                    ctx = listener.GetContext();
                }
                catch (Exception e)
                {
                    EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], e.Message.ToString() + "\r\n" + e.StackTrace.ToString(), EventLogEntryType.Error);

                    Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["ErrorRetryTimeout"]));
                    continue;
                }

                try
                {
                    // get params from ctx
                    short com = ParameterParser.parseShort(ctx.Request, "com", 1);
                    Int32 baud = ParameterParser.parseInt32(ctx.Request, "baud", 19200);
                    short ifacid = ParameterParser.parseShort(ctx.Request, "ifacid", 1);
                    string operation = ctx.Request.QueryString["operation"];
                    int result = -1000;

                    switch (operation)
                    {
                        case "ReadGasCard":
                            short klx = 1;
                            short kzt = 0;
                            byte[] khBuf = new byte[255];
                            byte[] tmBuf = new byte[255];
                            Int32 ql = 0;
                            Int32 cs = 0;
                            Int32 ljgql = 0;
                            Int32 bkcs = 0;
                            Int32 ljyql = 0;
                            Int32 syql = 0;

                            result = WaterCardReader.ReadGasCard(
                                ifacid,
                                com,
                                baud,
                                ref klx,
                                ref kzt,
                                khBuf,
                                tmBuf,
                                ref ql,
                                ref cs,
                                ref ljgql,
                                ref bkcs,
                                ref ljyql,
                                ref syql
                            );

                            String kh = ParameterParser.convertByteArrayToString(khBuf);
                            String tm = ParameterParser.convertByteArrayToString(tmBuf);

                            String ret = "{";
                            ret += string.Format(
                                "\"klx\":{0}," +
                                "\"kzt\":{1}," +
                                "\"kh\":\"{2}\"," +
                                "\"tm\":\"{3}\"," +
                                "\"ql\":{4}," +
                                "\"cs\":{5}," +
                                "\"ljgql\":{6}," +
                                "\"bkcs\":{7}," +
                                "\"ljyql\":{8}," +
                                "\"syql\":{9}," +
                                "\"result\":{10}",
                                klx,
                                kzt,
                                kh,
                                tm,
                                ql,
                                cs,
                                ljgql,
                                bkcs,
                                ljyql,
                                syql,
                                result
                            );
                            ret += "}";

                            if (result == 0)
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, ret);
                                ctx.Response.StatusCode = 200;
                            } else
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            break;

                        case "WriteNewCard":
                            result = WaterCardReader.WriteNewCard(
                                com,
                                baud,
                                ParameterParser.parseShort(ctx.Request, "klx", 1),
                                ParameterParser.parseShort(ctx.Request, "kzt", 0),
                                ParameterParser.convertStringToByteArray(ctx.Request.QueryString["kh"]),
                                ParameterParser.convertStringToByteArray(ctx.Request.QueryString["tm"]),
                                ParameterParser.parseShort(ctx.Request, "ql", 0),
                                ParameterParser.parseShort(ctx.Request, "cs", 0),
                                ParameterParser.parseInt32(ctx.Request, "ljgql", 0),
                                ParameterParser.parseShort(ctx.Request, "bkcs", 0),
                                ParameterParser.parseInt32(ctx.Request, "ljyql", 0),
                                ParameterParser.parseInt32(ctx.Request, "metertype", 0),
                                ParameterParser.parseInt32(ctx.Request, "Alarmnum", 0),
                                ParameterParser.parseInt32(ctx.Request, "ihoard", 0),
                                ParameterParser.parseInt32(ctx.Request, "metermode", 0),
                                ParameterParser.parseInt32(ctx.Request, "paramver", 0),
                                ParameterParser.convertStringToByteArray(ctx.Request.QueryString["pricestarttime"]),
                                ParameterParser.convertStringToByteArray(ctx.Request.QueryString["stepinfos"])
                            );

                            if (result == 0)
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"result\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            else
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            break;

                        case "WriteGasCard":
                            result = WaterCardReader.WriteGasCard(
                                com,
                                baud,
                                ParameterParser.parseShort(ctx.Request, "klx", 1),
                                ParameterParser.convertStringToByteArray(ctx.Request.QueryString["kh"]),
                                ParameterParser.parseShort(ctx.Request, "ql", 0),
                                ParameterParser.parseShort(ctx.Request, "cs", 0),
                                ParameterParser.parseInt32(ctx.Request, "ljgql", 0),
                                ParameterParser.parseInt32(ctx.Request, "bkcs", 0),
                                ParameterParser.parseInt32(ctx.Request, "metertype", 0),
                                ParameterParser.parseInt32(ctx.Request, "Alarmnum", 0),
                                ParameterParser.parseInt32(ctx.Request, "ihoard", 0),
                                ParameterParser.parseInt32(ctx.Request, "metermode", 0),
                                ParameterParser.parseInt32(ctx.Request, "paramver", 0),
                                ParameterParser.convertStringToByteArray(ctx.Request.QueryString["pricestarttime"]),
                                ParameterParser.convertStringToByteArray(ctx.Request.QueryString["stepinfos"])
                            );

                            if (result == 0)
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"result\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            else
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            break;

                        case "ClearMeterCard":
                            result = WaterCardReader.ClearMeterCard(
                                ParameterParser.parseInt(ctx.Request, "imetertype", 0)
                            );

                            if (result == 0)
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"result\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            else
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            break;

                        case "CheckMeterCard":
                            result = WaterCardReader.CheckMeterCard(
                                ParameterParser.parseInt(ctx.Request, "imetertype", 0)
                            );

                            if (result == 0)
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"result\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            else
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            break;

                        case "DatetimeMeterCard":
                            result = WaterCardReader.DatetimeMeterCard(
                                ParameterParser.parseInt(ctx.Request, "imetertype", 0),
                                ParameterParser.convertStringToByteArray(ctx.Request.QueryString["datetimes"])
                            );

                            if (result == 0)
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"result\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            else
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            break;

                        case "ParamsMeterCard":
                            result = WaterCardReader.ParamsMeterCard(
                                ParameterParser.parseInt(ctx.Request, "imetertype", 0),
                                ParameterParser.parseInt(ctx.Request, "Alarmnum", 0),
                                ParameterParser.parseInt(ctx.Request, "ihoard", 0)
                            );

                            if (result == 0)
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"result\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            else
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            break;

                        case "ModeMeterCard":
                            result = WaterCardReader.ModeMeterCard(
                                ParameterParser.parseInt(ctx.Request, "imetertype", 0),
                                ParameterParser.parseInt(ctx.Request, "iMode", 0),
                                ParameterParser.parseFloat(ctx.Request, "Price1", 0),
                                ParameterParser.parseFloat(ctx.Request, "Price2", 0),
                                ParameterParser.parseFloat(ctx.Request, "Price3", 0),
                                ParameterParser.parseFloat(ctx.Request, "Price4", 0),
                                ParameterParser.parseInt(ctx.Request, "StepMount1", 0),
                                ParameterParser.parseInt(ctx.Request, "StepMount2", 0),
                                ParameterParser.parseInt(ctx.Request, "StepMount3", 0)
                            );

                            if (result == 0)
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"result\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            else
                            {
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":" + result.ToString() + "}");
                                ctx.Response.StatusCode = 200;
                            }
                            break;

                        default:
                            jsonp = JsonpHandler.handle(ctx.Request, "{\"error\": \"Unknown operation.\"}");
                            ctx.Response.StatusCode = 200;
                            break;
                    }

                    /*
                    string version = ctx.Request.QueryString["ver"];

                    if (ConfigurationManager.AppSettings["Version"] != version)
                    {
                        jsonp = JsonpHandler.handle(ctx.Request, "{\"error\": \"API version mismatch.\"}");
                        ctx.Response.StatusCode = 200;
                        response(ctx.Response, jsonp);
                    }
                    */

                    response(ctx.Response, jsonp);
                }
                catch (Exception e)
                {
                    EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], e.Message.ToString() + "\r\n" + e.StackTrace.ToString(), EventLogEntryType.Error);

                    jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"" + e.Message.ToString() + "\r\n" + e.StackTrace.ToString() + "\"}");
                    ctx.Response.StatusCode = 200;
                    response(ctx.Response, jsonp);
                }
            }

            listener.Stop();
            EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "HTTP listener stopped.", EventLogEntryType.Information);
        }

        public void stop()
        {
            listenFlag = false;
        }

        private void response(HttpListenerResponse response, string jsonp)
        {
            response.AddHeader("Content-Security-Policy", "upgrade-insecure-requests");

            // write response        
            using (StreamWriter writer = new StreamWriter(response.OutputStream))
            {
                if (jsonp != null)
                    writer.Write(jsonp);

                writer.Close();
            }

            // close connection
            response.Close();
        }
    }

    public partial class CardReaderService : ServiceBase
    {
        private ListenerThread listenerThread;

        public CardReaderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // start listener thread
            listenerThread = new ListenerThread();
            Thread thread = new Thread(new ThreadStart(listenerThread.start));
            thread.Start();
        }

        protected override void OnStop()
        {
            listenerThread.stop();

            // dispose thread object and collect it - performance should be ok since we'll exit soon
            listenerThread = null;
            GC.Collect();
        }
    }
}
