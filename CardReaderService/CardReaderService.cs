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
            ZJWXCardReader zjwxCardReader = new ZJWXCardReader();
            HailiCardReader hailiCardReader = new HailiCardReader();

            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            listener.Prefixes.Add(ConfigurationManager.AppSettings["HttpEndpoint"]);

            listener.Start();
            listenFlag = true;
            EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "HTTP listener started.", EventLogEntryType.Information);

            while (listenFlag) 
            {
                HttpListenerContext ctx = listener.GetContext();
                string jsonp = null;

                // get params from ctx
                DeviceType type = DeviceType.CardReader;
                int _type = 0;
                int.TryParse(ctx.Request.QueryString["type"], out _type); // printer, cardreader
                type = (DeviceType)_type;
                string vendor = ctx.Request.QueryString["vendor"]; // YuChuan, ZJWX, Haili
                string operation = ctx.Request.QueryString["operation"];

                ZJWXOrderInfo orderInfo = new ZJWXOrderInfo();
                ZJWXCardMetaInfo metaInfo = new ZJWXCardMetaInfo();
                CardReaderResponseCode result;

                // do work
                switch (type)
                {
                    case DeviceType.Printer:
                        // not implemented yet
                        break;

                    case DeviceType.CardReader:
                        switch (vendor)
                        {
                            case "YuChuan":
                                // not implemented yet
                                break;

                            case "ZJWX":
                                switch (operation)
                                {
                                    case "setreader":

                                        break;
                                    case "checkreader":
                                        break;
                                    case "makecard":
                                        metaInfo.Deserialize(ctx.Request);
                                        result = zjwxCardReader.MakeCard(metaInfo);
                                        switch (result)
                                        {
                                            case CardReaderResponseCode.CommError:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Open port error\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            case CardReaderResponseCode.CardError:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Make error\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            case CardReaderResponseCode.Success:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"make\":\"OK\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            default:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Unknown error\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                        }
                                        break;
                                    case "clearcard":
                                        result = zjwxCardReader.ClearCard();
                                        switch (result)
                                        {
                                            case CardReaderResponseCode.CommError:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Open port error\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            case CardReaderResponseCode.CardError:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Clear error\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            case CardReaderResponseCode.Success:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"clear\":\"OK\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            default:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Unknown error\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                        }
                                        break;
                                    case "writecard":
                                        orderInfo.Deserialize(ctx.Request);
                                        result = zjwxCardReader.WriteCard(orderInfo);
                                        switch (result)
                                        {
                                            case CardReaderResponseCode.CommError:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Open port error\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            case CardReaderResponseCode.WriteError:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Write error\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            case CardReaderResponseCode.Success:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"write\":\"OK\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            default:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Unknown error\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                        }
                                        break;
                                    case "readcard":
                                        CardInfo cardInfo = zjwxCardReader.ReadCard();
                                        if (cardInfo == null)
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Read error\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        else
                                        {
                                            string cardInfoStr = cardInfo.Serialize();
                                            jsonp = JsonpHandler.handle(ctx.Request, cardInfoStr);
                                            ctx.Response.StatusCode = 200;
                                        }
                                        break;
                                }
                                break;

                            case "Haili":
                                switch (operation)
                                {
                                    case "setreader":
                                        int port;
                                        int baudrate;

                                        if (ctx.Request.QueryString["port"] != null)
                                        {
                                            if (int.TryParse(ctx.Request.QueryString["port"], out port) == true)
                                                hailiCardReader.Port = port;
                                            else
                                            {
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Port error\"}");
                                                ctx.Response.StatusCode = 200;
                                            }
                                        }

                                        if (ctx.Request.QueryString["baudrate"] != null)
                                        {
                                            if (int.TryParse(ctx.Request.QueryString["baudrate"], out baudrate) == true)
                                                hailiCardReader.Baudrate = baudrate;
                                            else
                                            {
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Port error\"}");
                                                ctx.Response.StatusCode = 200;
                                            }
                                        }

                                        jsonp = JsonpHandler.handle(ctx.Request, "{\"set\":\"OK\"}");
                                        ctx.Response.StatusCode = 200;
                                        break;
                                    case "readcard":
                                        CardInfo cardInfo = hailiCardReader.ReadCard();
                                        if (cardInfo == null)
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Read error\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        else
                                        {
                                            string cardInfoStr = cardInfo.Serialize();
                                            jsonp = JsonpHandler.handle(ctx.Request, cardInfoStr);
                                            ctx.Response.StatusCode = 200;
                                        }
                                        break;

                                    case "writecard":
                                        HailiOrderInfo order = new HailiOrderInfo();
                                        order.Deserialize(ctx.Request);
                                        order.Kh = Crypto.decode(order.Kh, Crypto.keyseed);
                                        order.Kh = order.Kh.Split('-')[0];

                                        result = hailiCardReader.WriteCard(order);
                                        if (result == CardReaderResponseCode.Success)
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"write\":\"OK\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        else
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Write error\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        break;

                                    case "clearcard":
                                        result = hailiCardReader.ClearCard();
                                        if (result == CardReaderResponseCode.Success)
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"clear\":\"OK\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        else
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Clear error\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        break;

                                    case "makecard":
                                        HailiMetaInfo meta = new HailiMetaInfo();                                     
                                        meta.Deserialize(ctx.Request);

                                        result = hailiCardReader.MakeCard(meta);
                                        if (result == CardReaderResponseCode.Success)
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"make\":\"OK\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        else
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Make error\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        break;

                                    case "checkreader":
                                        result = hailiCardReader.CheckReader();
                                        if (result == CardReaderResponseCode.Success)
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"check\":\"OK\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        else
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Check error\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        break;

                                    case "clearwatch":
                                        HailiWatchInfo watchInfo = new HailiWatchInfo();
                                        watchInfo.Deserialize(ctx.Request);

                                        result = hailiCardReader.MakeInitCard(watchInfo);
                                        if (result == CardReaderResponseCode.Success)
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"make\":\"OK\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        else
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Make error\"}");
                                            ctx.Response.StatusCode = 200;
                                        }
                                        break;
                                }
                                break;

                            default:
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Vendor not found\"}");
                                ctx.Response.StatusCode = 200;
                                break;
                        }
                        break;

                    default:
                        jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Device type not found\"}");
                        ctx.Response.StatusCode = 200;
                        break;
                }        

                // write response        
                using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
                {
                    if (jsonp != null)
                        writer.Write(jsonp);

                    writer.Close();
                }

                ctx.Response.Close();
            }

            listener.Stop();
            EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "HTTP listener stopped.", EventLogEntryType.Information);
        }

        public void stop()
        {
            listenFlag = false;
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
