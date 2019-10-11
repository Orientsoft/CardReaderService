using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CardReaderService
{
    public class EnnewCardInfo : CardInfo
    {
        public short Com { get; set; }
        public long Baud { get; set; }
        public short Klx { get; set; }
        public short Kzt { get; set; }
        public string Kh { get; set; }
        public string Tm { get; set; }
        public Int32 Ql { get; set; }
        public Int32 Cs { get; set; }
        public Int32 Ljgql { get; set; }
        public Int32 Bkcs { get; set; }
        public Int32 Ljyql { get; set; }
        public Int32 Syql { get; set; }

        public override string Serialize()
        {
            string ret = "{";
            ret += string.Format("\"com\":{0}," +
                "\"baud\":{1}," +
                "\"klx\":{2}," +
                "\"kzt\":{3}," +
                "\"kh\":{4}," +
                "\"tm\":\"{5}\"," +
                "\"ql\":{6}," +
                "\"cs\":{7}," +
                "\"ljgql\":{8}," +
                "\"bkcs\":{9}," +
                "\"ljyql\":{10}," +
                "\"syql\":{11}",
                this.Com,
                this.Baud,
                this.Klx,
                this.Kzt,
                this.Kh,
                this.Tm,
                this.Ql,
                this.Cs,
                this.Ljgql,
                this.Bkcs,
                this.Ljyql,
                this.Syql
                );
            ret += "}";

            return ret;
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            bool ret = false;

            if (request.QueryString["com"] != null)
            {
                short com;
                if (short.TryParse(request.QueryString["com"], out com) == true)
                    this.Com = com;
                else
                    return ret;
            }

            if (request.QueryString["baud"] != null)
            {
                Int32 baud;
                if (Int32.TryParse(request.QueryString["baud"], out baud) == true)
                    this.Baud = baud;
                else
                    return ret;
            }

            if (request.QueryString["klx"] != null)
            {
                short klx;
                if (short.TryParse(request.QueryString["klx"], out klx) == true)
                    this.Klx = klx;
                else
                    return ret;
            }

            if (request.QueryString["kzt"] != null)
            {
                short kzt;
                if (short.TryParse(request.QueryString["kzt"], out kzt) == true)
                    this.Kzt = kzt;
                else
                    return ret;
            }

            if (request.QueryString["kh"] != null)
            {
                this.Kh = request.QueryString["kh"];
            }

            if (request.QueryString["tm"] != null)
            {
                this.Tm = request.QueryString["tm"];
            }

            if (request.QueryString["ql"] != null)
            {
                Int32 ql;
                if (Int32.TryParse(request.QueryString["ql"], out ql) == true)
                    this.Ql = ql;
                else
                    return ret;
            }

            if (request.QueryString["cs"] != null)
            {
                Int32 cs;
                if (Int32.TryParse(request.QueryString["cs"], out cs) == true)
                    this.Cs = cs;
                else
                    return ret;
            }

            if (request.QueryString["ljgql"] != null)
            {
                Int32 ljgql;
                if (Int32.TryParse(request.QueryString["ljgql"], out ljgql) == true)
                    this.Ljgql = ljgql;
                else
                    return ret;
            }

            if (request.QueryString["bkcs"] != null)
            {
                Int32 bkcs;
                if (Int32.TryParse(request.QueryString["bkcs"], out bkcs) == true)
                    this.Bkcs = bkcs;
                else
                    return ret;
            }

            if (request.QueryString["ljyql"] != null)
            {
                Int32 ljyql;
                if (Int32.TryParse(request.QueryString["ljyql"], out ljyql) == true)
                    this.Ljyql = ljyql;
                else
                    return ret;
            }

            if (request.QueryString["syql"] != null)
            {
                Int32 syql;
                if (Int32.TryParse(request.QueryString["syql"], out syql) == true)
                    this.Syql = syql;
                else
                    return ret;
            }

            ret = true;
            return ret;
        }
    }

    public class EnnewOrderInfo : OrderInfo
    {
        public short Com { get; set; }
        public long Baud { get; set; }
        public short Klx { get; set; }
        public short Kzt { get; set; }
        public string Kh { get; set; }
        public string Tm { get; set; }
        public Int32 Ql { get; set; }
        public Int32 Cs { get; set; }
        public Int32 Ljgql { get; set; }
        public Int32 Bkcs { get; set; }
        public Int32 Ljyql { get; set; }
        public Int32 Syql { get; set; }

        public override string Serialize()
        {
            string ret = "{";
            ret += string.Format("\"com\":{0}," +
                "\"baud\":{1}," +
                "\"klx\":{2}," +
                "\"kzt\":{3}," +
                "\"kh\":{4}," +
                "\"tm\":{5}," +
                "\"ql\":{6}," +
                "\"cs\":{7}," +
                "\"ljgql\":{8}," +
                "\"bkcs\":{9}," +
                "\"ljyql\":{10}," +
                "\"syql\":{11}",
                this.Com,
                this.Baud,
                this.Klx,
                this.Kzt,
                this.Kh,
                this.Tm,
                this.Ql,
                this.Cs,
                this.Ljgql,
                this.Bkcs,
                this.Ljyql,
                this.Syql
                );
            ret += "}";

            return ret;
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            bool ret = false;

            if (request.QueryString["com"] != null)
            {
                short com;
                if (short.TryParse(request.QueryString["com"], out com) == true)
                    this.Com = com;
                else
                    return ret;
            }

            if (request.QueryString["baud"] != null)
            {
                Int32 baud;
                if (Int32.TryParse(request.QueryString["baud"], out baud) == true)
                    this.Baud = baud;
                else
                    return ret;
            }

            if (request.QueryString["klx"] != null)
            {
                short klx;
                if (short.TryParse(request.QueryString["klx"], out klx) == true)
                    this.Klx = klx;
                else
                    return ret;
            }

            if (request.QueryString["kzt"] != null)
            {
                short kzt;
                if (short.TryParse(request.QueryString["kzt"], out kzt) == true)
                    this.Kzt = kzt;
                else
                    return ret;
            }

            if (request.QueryString["kh"] != null)
            {
                this.Kh = request.QueryString["kh"];
            }

            if (request.QueryString["tm"] != null)
            {
                this.Tm = request.QueryString["tm"];
            }

            if (request.QueryString["ql"] != null)
            {
                Int32 ql;
                if (Int32.TryParse(request.QueryString["ql"], out ql) == true)
                    this.Ql = ql;
                else
                    return ret;
            }

            if (request.QueryString["cs"] != null)
            {
                Int32 cs;
                if (Int32.TryParse(request.QueryString["cs"], out cs) == true)
                    this.Cs = cs;
                else
                    return ret;
            }

            if (request.QueryString["ljgql"] != null)
            {
                Int32 ljgql;
                if (Int32.TryParse(request.QueryString["ljgql"], out ljgql) == true)
                    this.Ljgql = ljgql;
                else
                    return ret;
            }

            if (request.QueryString["bkcs"] != null)
            {
                Int32 bkcs;
                if (Int32.TryParse(request.QueryString["bkcs"], out bkcs) == true)
                    this.Bkcs = bkcs;
                else
                    return ret;
            }

            if (request.QueryString["ljyql"] != null)
            {
                Int32 ljyql;
                if (Int32.TryParse(request.QueryString["ljyql"], out ljyql) == true)
                    this.Ljyql = ljyql;
                else
                    return ret;
            }

            if (request.QueryString["syql"] != null)
            {
                Int32 syql;
                if (Int32.TryParse(request.QueryString["syql"], out syql) == true)
                    this.Syql = syql;
                else
                    return ret;
            }

            ret = true;
            return ret;
        }
    }

    public class EnnewWatchInfo : WatchInfo
    {
        public short Com { get; set; }
        public long Baud { get; set; }
        public short Klx { get; set; }
        public string Kh { get; set; }

        public override string Serialize()
        {
            string ret = "{";
            ret += string.Format("\"com\":{0}," +
                "\"baud\":{1}," +
                "\"klx\":{2}," +
                "\"kh\":{3}",
                this.Com,
                this.Baud,
                this.Klx,
                this.Kh
                );
            ret += "}";

            return ret;
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            bool ret = false;

            if (request.QueryString["com"] != null)
            {
                short com;
                if (short.TryParse(request.QueryString["com"], out com) == true)
                    this.Com = com;
                else
                    return ret;
            }

            if (request.QueryString["baud"] != null)
            {
                Int32 baud;
                if (Int32.TryParse(request.QueryString["baud"], out baud) == true)
                    this.Baud = baud;
                else
                    return ret;
            }

            if (request.QueryString["klx"] != null)
            {
                short klx;
                if (short.TryParse(request.QueryString["klx"], out klx) == true)
                    this.Klx = klx;
                else
                    return ret;
            }

            if (request.QueryString["kh"] != null)
            {
                this.Kh = request.QueryString["kh"];
            }

            ret = true;
            return ret;
        }
    }


    public class EnnewMetaInfo : CardMetaInfo
    {
        public short Com { get; set; }
        public long Baud { get; set; }
        public short Klx { get; set; }
        public short Kzt { get; set; }
        public string Kh { get; set; }
        public string Tm { get; set; }
        public Int32 Ql { get; set; }
        public Int32 Cs { get; set; }
        public Int32 Ljgql { get; set; }
        public Int32 Bkcs { get; set; }
        public Int32 Ljyql { get; set; }

        public override string Serialize()
        {
            string ret = "{";
            ret += string.Format("\"com\":{0}," +
                "\"baud\":{1}," +
                "\"klx\":{2}," +
                "\"kzt\":{3}," +
                "\"kh\":{4}," +
                "\"tm\":\"{5}\"," +
                "\"ql\":{6}," +
                "\"cs\":{7}," +
                "\"ljgql\":{8}," +
                "\"bkcs\":{9}," +
                "\"ljyql\":{10}",
                this.Com,
                this.Baud,
                this.Klx,
                this.Kzt,
                this.Kh,
                this.Tm,
                this.Ql,
                this.Cs,
                this.Ljgql,
                this.Bkcs,
                this.Ljyql
                );
            ret += "}";

            return ret;
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            bool ret = false;

            if (request.QueryString["com"] != null)
            {
                short com;
                if (short.TryParse(request.QueryString["com"], out com) == true)
                    this.Com = com;
                else
                    return ret;
            }

            if (request.QueryString["baud"] != null)
            {
                Int32 baud;
                if (Int32.TryParse(request.QueryString["baud"], out baud) == true)
                    this.Baud = baud;
                else
                    return ret;
            }

            if (request.QueryString["klx"] != null)
            {
                short klx;
                if (short.TryParse(request.QueryString["klx"], out klx) == true)
                    this.Klx = klx;
                else
                    return ret;
            }

            if (request.QueryString["kzt"] != null)
            {
                short kzt;
                if (short.TryParse(request.QueryString["kzt"], out kzt) == true)
                    this.Kzt = kzt;
                else
                    return ret;
            }

            if (request.QueryString["kh"] != null)
            {
                this.Kh = request.QueryString["kh"];
            }

            if (request.QueryString["tm"] != null)
            {
                this.Tm = request.QueryString["tm"];
            }

            if (request.QueryString["ql"] != null)
            {
                Int32 ql;
                if (Int32.TryParse(request.QueryString["ql"], out ql) == true)
                    this.Ql = ql;
                else
                    return ret;
            }

            if (request.QueryString["cs"] != null)
            {
                Int32 cs;
                if (Int32.TryParse(request.QueryString["cs"], out cs) == true)
                    this.Cs = cs;
                else
                    return ret;
            }

            if (request.QueryString["ljgql"] != null)
            {
                Int32 ljgql;
                if (Int32.TryParse(request.QueryString["ljgql"], out ljgql) == true)
                    this.Ljgql = ljgql;
                else
                    return ret;
            }

            if (request.QueryString["bkcs"] != null)
            {
                Int32 bkcs;
                if (Int32.TryParse(request.QueryString["bkcs"], out bkcs) == true)
                    this.Bkcs = bkcs;
                else
                    return ret;
            }

            if (request.QueryString["ljyql"] != null)
            {
                Int32 ljyql;
                if (Int32.TryParse(request.QueryString["ljyql"], out ljyql) == true)
                    this.Ljyql = ljyql;
                else
                    return ret;
            }

            ret = true;
            return ret;
        }
    }

    class EnnewCardReader : CardReaderAdpator
    {
        // DLL imports
        [DllImportAttribute("Enn.dll", EntryPoint = "ReadGasCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadGasCard(short com, Int32 baud, ref short klx, ref short kzt, byte[] kh, byte[] tm, ref Int32 ql, ref Int32 cs, ref Int32 ljgql, ref Int32 bkcs, ref Int32 ljyql, ref Int32 syql, ref short ysb);

        [DllImportAttribute("Enn.dll", EntryPoint = "WriteNewCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteNewCard(short com, Int32 baud, short klx, short kzt, byte[] kh, byte[] tm, Int32 ql, short cs, Int32 ljgql, short bkcs, Int32 ljyql);

        [DllImportAttribute("Enn.dll", EntryPoint = "WriteGasCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteGasCard(short com, Int32 baud, short klx, byte[] kh, short ql, short cs, Int32 ljgql);

        [DllImportAttribute("Enn.dll", EntryPoint = "FormatGasCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int FormatGasCard(short com, Int32 baud);

        [DllImportAttribute("Enn.dll", EntryPoint = "CheckGasCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int CheckGasCard(short com, Int32 baud);

        [DllImportAttribute("Enn.dll", EntryPoint = "makeInitCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int makeInitCard(short com, Int32 baud, short klx, byte[] kh);

        public override string Id { get; }
        public override DeviceType Type { get; }
        public override string Vendor { get; }

        public override int Port { get; set; }
        public override int Baudrate { get; set; }

        public EnnewCardReader()
        {
            this.Type = DeviceType.CardReader;
            this.Vendor = "Ennew";
        }

        // interface
        public override CardReaderResponseCode CheckReader()
        {
            int result = CheckGasCard((short)this.Port, this.Baudrate);
            if (result == 0 || result == 1)
            {
                return CardReaderResponseCode.Success;
            }
            else
            {
                return CardReaderResponseCode.CardError;
            }
        }

        public override CardInfo ReadCard()
        {
            byte[] khBytes = new byte[255];
            byte[] tmBytes = new byte[255];

            short klx = 0;
            short kzt = 0;
            Int32 ql = 0;
            Int32 cs = 0;
            Int32 ljgql = 0;
            Int32 bkcs = 0;
            Int32 ljyql = 0;
            Int32 syql = 0;
            short ysb = 0;

            EnnewCardInfo info = new EnnewCardInfo();

            int ret = ReadGasCard((short)this.Port, this.Baudrate, ref klx, ref kzt, khBytes, tmBytes, ref ql, ref cs, ref ljgql, ref bkcs, ref ljyql, ref syql, ref ysb);

            if (ret >= 0)
            {
                info.Klx = klx;
                info.Kzt = kzt;
                info.Ql = ql;
                info.Cs = cs;
                info.Ljgql = ljgql;
                info.Bkcs = bkcs;
                info.Ljyql = ljyql;
                info.Syql = syql;

                info.Kh = Encoding.Default.GetString(khBytes).Trim('\0');
                info.Tm = Encoding.Default.GetString(tmBytes).Trim('\0');

                return info;
            }
            else
            {
                EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "Operation failed. Return: " + ret.ToString(), EventLogEntryType.Error);

                info.Klx = -1;
                info.Kzt = (short)ret;

                return info;
            }
        }

        public override CardReaderResponseCode WriteCard(OrderInfo order)
        {
            EnnewOrderInfo info = (EnnewOrderInfo)order;
            int ret = WriteGasCard((short)this.Port, this.Baudrate, (short)info.Klx, Encoding.Default.GetBytes(info.Kh), (Int16)info.Ql, (short)info.Cs, info.Ljgql);

            if (ret == 0)
                return CardReaderResponseCode.Success;
            else
            {
                EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "Operation failed. Result: " + ret.ToString(), EventLogEntryType.Error);
                return CardReaderResponseCode.WriteError;
            }
        }

        public override CardReaderResponseCode MakeCard(CardMetaInfo metaInfo)
        {
            EnnewMetaInfo meta = (EnnewMetaInfo)metaInfo;
            int result = WriteNewCard((short)this.Port, this.Baudrate, meta.Klx, meta.Kzt, Encoding.Default.GetBytes(meta.Kh), Encoding.Default.GetBytes(meta.Tm), meta.Ql, (short)meta.Cs, meta.Ljgql, (short)meta.Bkcs, meta.Ljyql);
            if (result >= 0)
                return CardReaderResponseCode.Success;
            else
            {
                EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "Operation failed. Result: " + result.ToString(), EventLogEntryType.Error);
                return CardReaderResponseCode.WriteError;
            }
        }

        public override CardReaderResponseCode ClearCard()
        {
            int result = FormatGasCard((short)this.Port, this.Baudrate);
            if (result >= 0)
            {
                return CardReaderResponseCode.Success;
            }
            else
            {
                EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "Operation failed. Result: " + result.ToString(), EventLogEntryType.Error);
                return CardReaderResponseCode.CardError;
            }
        }

        public override WatchInfo ReadWatchInfo()
        {
            throw new NotImplementedException();
        }

        public CardReaderResponseCode MakeInitCard(EnnewWatchInfo watchInfo)
        {
            int result = makeInitCard((short)this.Port, this.Baudrate, watchInfo.Klx, Encoding.Default.GetBytes(watchInfo.Kh));
            if (result >= 0)
            {
                return CardReaderResponseCode.Success;
            }
            else
            {
                EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "Operation failed. Result: " + result.ToString(), EventLogEntryType.Error);
                return CardReaderResponseCode.WriteError;
            }
        }
    }
}
