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

// P/Invoke deps


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
                string vendor = ctx.Request.QueryString["vendor"]; // YuChuan, ZJWX
                string operation = ctx.Request.QueryString["operation"];

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
                                    case "checkreader":
                                        break;
                                    case "makecard":
                                        break;
                                    case "clearcard":
                                        break;
                                    case "writecard":
                                        ZJWXOrderInfo orderInfo = new ZJWXOrderInfo();
                                        orderInfo.Deserialize(ctx.Request);
                                        CardReaderResponseCode result = zjwxCardReader.WriteCard(orderInfo);
                                        switch (result)
                                        {
                                            case CardReaderResponseCode.CommError:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Open port error\"}");
                                                ctx.Response.StatusCode = 500;
                                                break;
                                            case CardReaderResponseCode.WriteError:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Write error\"}");
                                                ctx.Response.StatusCode = 500;
                                                break;
                                            case CardReaderResponseCode.Success:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"write\":\"OK\"}");
                                                ctx.Response.StatusCode = 200;
                                                break;
                                            default:
                                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Unknown error\"}");
                                                ctx.Response.StatusCode = 500;
                                                break;
                                        }
                                        break;
                                    case "readcard":
                                        CardInfo cardInfo = zjwxCardReader.ReadCard();
                                        if (cardInfo == null)
                                        {
                                            jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Read error\"}");
                                            ctx.Response.StatusCode = 500;
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

                            default:
                                jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Vendor not found\"}");
                                ctx.Response.StatusCode = 404;
                                break;
                        }
                        break;

                    default:
                        jsonp = JsonpHandler.handle(ctx.Request, "{\"error\":\"Device type not found\"}");
                        ctx.Response.StatusCode = 404;
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
