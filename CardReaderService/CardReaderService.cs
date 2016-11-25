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
            string script = callback + "({" + resp + "})";

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
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            listener.Prefixes.Add("http://localhost:29527/cardreader/");

            listener.Start();
            listenFlag = true;
            EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "HTTP listener started.", EventLogEntryType.Information);

            while (listenFlag) 
            {
                HttpListenerContext ctx = listener.GetContext();
                
                // get params from ctx
                string name = ctx.Request.QueryString["name"];

                // do work
                // string greetings = "Hello, " + name;
                string greetings = string.Format("\"greetings\":\"{0}\"", "Hello, " + name);
                string jsonp = JsonpHandler.handle(ctx.Request, greetings);

                // write response
                ctx.Response.StatusCode = 200;
                using (StreamWriter writer = new StreamWriter(ctx.Response.OutputStream))
                {
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
