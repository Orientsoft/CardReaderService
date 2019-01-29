using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CardReaderService
{
    public class RxCardInfo : CardInfo
    {
        private short com;
        private long baud;
        private short klx;
        private short kzt;
        private string kh;
        private string tm;
        private Int32 ql;
        private Int32 cs;
        private Int32 ljgql;
        private Int32 bkcs;
        private Int32 ljyql;
        private Int32 syql;

        public short Com
        {
            get
            {
                return com;
            }

            set
            {
                com = value;
            }
        }

        public long Baud
        {
            get
            {
                return baud;
            }

            set
            {
                baud = value;
            }
        }

        public short Klx
        {
            get
            {
                return klx;
            }

            set
            {
                klx = value;
            }
        }

        public short Kzt
        {
            get
            {
                return kzt;
            }

            set
            {
                kzt = value;
            }
        }

        public string Kh
        {
            get
            {
                return kh;
            }

            set
            {
                kh = value;
            }
        }

        public string Tm
        {
            get
            {
                return tm;
            }

            set
            {
                tm = value;
            }
        }

        public int Ql
        {
            get
            {
                return ql;
            }

            set
            {
                ql = value;
            }
        }

        public int Cs
        {
            get
            {
                return cs;
            }

            set
            {
                cs = value;
            }
        }

        public int Ljgql
        {
            get
            {
                return ljgql;
            }

            set
            {
                ljgql = value;
            }
        }

        public int Bkcs
        {
            get
            {
                return bkcs;
            }

            set
            {
                bkcs = value;
            }
        }

        public int Ljyql
        {
            get
            {
                return ljyql;
            }

            set
            {
                ljyql = value;
            }
        }

        public int Syql
        {
            get
            {
                return syql;
            }

            set
            {
                syql = value;
            }
        }

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
                    this.kzt = kzt;
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

    public class RxOrderInfo : OrderInfo
    {
        private short com;
        private long baud;
        private short klx;
        private short kzt;
        private string kh;
        private string tm;
        private Int32 ql;
        private Int32 cs;
        private Int32 ljgql;
        private Int32 bkcs;
        private Int32 ljyql;
        private Int32 syql;

        public short Com
        {
            get
            {
                return com;
            }

            set
            {
                com = value;
            }
        }

        public long Baud
        {
            get
            {
                return baud;
            }

            set
            {
                baud = value;
            }
        }

        public short Klx
        {
            get
            {
                return klx;
            }

            set
            {
                klx = value;
            }
        }

        public short Kzt
        {
            get
            {
                return kzt;
            }

            set
            {
                kzt = value;
            }
        }

        public string Kh
        {
            get
            {
                return kh;
            }

            set
            {
                kh = value;
            }
        }

        public string Tm
        {
            get
            {
                return tm;
            }

            set
            {
                tm = value;
            }
        }

        public int Ql
        {
            get
            {
                return ql;
            }

            set
            {
                ql = value;
            }
        }

        public int Cs
        {
            get
            {
                return cs;
            }

            set
            {
                cs = value;
            }
        }

        public int Ljgql
        {
            get
            {
                return ljgql;
            }

            set
            {
                ljgql = value;
            }
        }

        public int Bkcs
        {
            get
            {
                return bkcs;
            }

            set
            {
                bkcs = value;
            }
        }

        public int Ljyql
        {
            get
            {
                return ljyql;
            }

            set
            {
                ljyql = value;
            }
        }

        public int Syql
        {
            get
            {
                return syql;
            }

            set
            {
                syql = value;
            }
        }

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
                    this.kzt = kzt;
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

    public class RxWatchInfo : WatchInfo
    {
        private short com;
        private long baud;
        private short klx;
        private string kh;

        public short Com
        {
            get
            {
                return com;
            }

            set
            {
                com = value;
            }
        }

        public long Baud
        {
            get
            {
                return baud;
            }

            set
            {
                baud = value;
            }
        }

        public short Klx
        {
            get
            {
                return klx;
            }

            set
            {
                klx = value;
            }
        }

        public string Kh
        {
            get
            {
                return kh;
            }

            set
            {
                kh = value;
            }
        }

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


    public class RxMetaInfo : CardMetaInfo
    {
        private short com;
        private long baud;
        private short klx;
        private short kzt;
        private string kh;
        private string tm;
        private Int32 ql;
        private Int32 cs;
        private Int32 ljgql;
        private Int32 bkcs;
        private Int32 ljyql;

        public short Com
        {
            get
            {
                return com;
            }

            set
            {
                com = value;
            }
        }

        public long Baud
        {
            get
            {
                return baud;
            }

            set
            {
                baud = value;
            }
        }

        public short Klx
        {
            get
            {
                return klx;
            }

            set
            {
                klx = value;
            }
        }

        public short Kzt
        {
            get
            {
                return kzt;
            }

            set
            {
                kzt = value;
            }
        }

        public string Kh
        {
            get
            {
                return kh;
            }

            set
            {
                kh = value;
            }
        }

        public string Tm
        {
            get
            {
                return tm;
            }

            set
            {
                tm = value;
            }
        }

        public int Ql
        {
            get
            {
                return ql;
            }

            set
            {
                ql = value;
            }
        }

        public int Cs
        {
            get
            {
                return cs;
            }

            set
            {
                cs = value;
            }
        }

        public int Ljgql
        {
            get
            {
                return ljgql;
            }

            set
            {
                ljgql = value;
            }
        }

        public int Bkcs
        {
            get
            {
                return bkcs;
            }

            set
            {
                bkcs = value;
            }
        }

        public int Ljyql
        {
            get
            {
                return ljyql;
            }

            set
            {
                ljyql = value;
            }
        }

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
                    this.kzt = kzt;
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

    public class RxCardReader : CardReaderAdpator
    {
        // DLL imports
        [DllImportAttribute("LtA1.dll", EntryPoint = "ReadGasCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int ReadGasCard(short com, Int32 baud, ref short klx, ref short kzt, byte[] kh, byte[] tm, ref Int32 ql, ref short cs, ref Int32 ljgql, ref short bkcs, ref Int32 ljyql, ref Int32 syql);

        [DllImportAttribute("LtA1.dll", EntryPoint = "WriteNewCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteNewCard(short com, Int32 baud, short klx, short kzt, byte[] kh, byte[] tm, Int32 ql, short cs, Int32 ljgql, short bkcs, Int32 ljyql);

        [DllImportAttribute("LtA1.dll", EntryPoint = "WriteGasCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int WriteGasCard(short com, Int32 baud, short klx, byte[] kh, short ql, short cs, Int32 ljgql);

        [DllImportAttribute("LtA1.dll", EntryPoint = "FormatGasCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int FormatGasCard(short com, Int32 baud, byte[] kmm, short klx, byte[] kh, byte[] dqdm);

        [DllImportAttribute("LtA1.dll", EntryPoint = "CheckGasCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int CheckGasCard(short com, Int32 baud);

        [DllImportAttribute("LtA1.dll", EntryPoint = "MakeInitCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int makeInitCard(short com, Int32 baud, short klx, byte[] kh);

        private string id;
        private DeviceType type;
        private string vendor;

        private int port;
        private int baudrate;

        public override string Id
        {
            get
            {
                return id;
            }
        }

        public override DeviceType Type
        {
            get
            {
                return type;
            }
        }

        public override string Vendor
        {
            get
            {
                return vendor;
            }
        }

        public override int Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        public override int Baudrate
        {
            get
            {
                return baudrate;
            }

            set
            {
                baudrate = value;
            }
        }

        public RxCardReader()
        {
            this.type = DeviceType.CardReader;
            this.vendor = "Rx";
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

            /*
            short klx = 0;
            short kzt = 0;
            Int32 ql = 0;
            Int32 cs = 0;
            Int32 ljgql = 0;
            Int32 bkcs = 0;
            Int32 ljyql = 0;
            Int32 syql = 0;
            */

            short klx = 80;
            short kzt = 1;
            Int32 ql = 5;
            short cs = 2;
            Int32 ljgql = 25;
            short bkcs = 0;
            Int32 ljyql = 0;
            Int32 syql = 0;

            RxCardInfo info = new RxCardInfo();

            int ret = ReadGasCard((short)3, 9600, ref klx, ref kzt, khBytes, tmBytes, ref ql, ref cs, ref ljgql, ref bkcs, ref ljyql, ref syql);

            if (ret >= 0)
            {
                info.Klx = 80; // ignore klx since they only provide -1
                info.Kzt = kzt;
                info.Ql = ql / 100;
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
            RxOrderInfo info = (RxOrderInfo)order;
            int ret = WriteGasCard((short)this.Port, this.Baudrate, (short)info.Klx, Encoding.Default.GetBytes(info.Kh), (Int16)(info.Ql * 100), (short)info.Cs, info.Ljgql);

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
            RxMetaInfo meta = (RxMetaInfo)metaInfo;
            int result = WriteNewCard((short)this.Port, this.Baudrate, meta.Klx, meta.Kzt, Encoding.Default.GetBytes(meta.Kh), Encoding.Default.GetBytes(meta.Tm), meta.Ql * 100, (short)meta.Cs, meta.Ljgql, (short)meta.Bkcs, meta.Ljyql);
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
            string kmm = "";
            string kh = "";
            string dqdm = "";

            int result = FormatGasCard((short)this.Port, this.Baudrate, Encoding.Default.GetBytes(kmm), 80, Encoding.Default.GetBytes(kh), Encoding.Default.GetBytes(dqdm));
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

        public CardReaderResponseCode MakeInitCard(RxWatchInfo watchInfo)
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
