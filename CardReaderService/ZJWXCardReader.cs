using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CardReaderService
{
    // data structure & functional implementation for ZJWX library

    public class ZJWXLadderInfo : LadderInfo
    {
        private int priceNo;
        private string execDate;
        private string startoverDate;
        private int[] price; // in cents
        private int[] vol;

        public ZJWXLadderInfo()
        {
            // do nothing
        }

        public ZJWXLadderInfo(int _priceNo, string _execDate, string _startoverDate, int[] _price, int[] _vol)
        {
            priceNo = _priceNo;
            execDate = _execDate;
            startoverDate = _startoverDate;
            price = _price;
            vol = _vol;
        }

        public override int PriceNo
        {
            get
            {
                return priceNo;
            }

            set
            {
                priceNo = value;
            }
        }

        public override string ExecDate
        {
            get
            {
                return execDate;
            }

            set
            {
                execDate = value;
            }
        }

        public override string StartoverDate
        {
            get
            {
                return startoverDate;
            }

            set
            {
                startoverDate = value;
            }
        }

        public override int[] Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public override int[] Vol
        {
            get
            {
                return vol;
            }

            set
            {
                vol = value;
            }
        }

        public override string getLadderString()
        {
            string ladderStr = string.Format("{0}|{1}|{2}", this.PriceNo, this.ExecDate, this.StartoverDate);

            for (int i = 0; i < this.price.Length; i++)
            {
                ladderStr += "|" + this.Price[i];
                ladderStr += "|" + this.Vol[i];
            }

            return ladderStr;
        }

        public override bool parseLadderString(string ladderStr)
        {
            char[] sp = { '|' };
            string[] ladderArr = ladderStr.Split(sp);

            int _priceNo = 0;
            int.TryParse(ladderArr[0], out _priceNo);
            this.PriceNo = _priceNo;

            this.ExecDate = ladderArr[1];
            this.StartoverDate = ladderArr[2];

            this.Price = new int[(ladderArr.Length - 3) / 2];
            this.Vol = new int[(ladderArr.Length - 3) / 2];

            for (int i = 0; i < (ladderArr.Length - 3) / 2; i++)
            {
                int _price = 0;
                int _vol = 0;
                int.TryParse(ladderArr[i * 2 + 3], out _price);
                this.Price[i] = _price;
                int.TryParse(ladderArr[i * 2 + 4], out _vol);
                this.Vol[i] = _vol;
            }

            return true;
        }

        public override string Serialize()
        {
            string json = "{";

            json += "\"priceNo\":" + this.PriceNo + "," +
                "\"execDate\":\"" + this.ExecDate + "\"," +
                "\"startoverDate\":\"" + this.StartoverDate + "\",";
                
            json += "\"price\":[";

            for (int i = 0; i < price.Length; i++)
            {
                if (i < price.Length - 1)
                    json += price[i].ToString() + ",";
                else
                    json += price[i];
            }

            json += "],\"vol\":[";

            for (int i = 0; i < vol.Length; i++)
            {
                if (i < vol.Length - 1)
                    json += vol[i].ToString() + ",";
                else
                    json += vol[i];
            }

            json += "]}";

            return json;
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class ZJWXCardInfo : CardInfo
    {
        private string watchType;
        private string cardType;
        private string cardNo;
        private int orderAmount;
        private int orderNo;
        private int watchLimit;
        private int overdraftAmount;
        private int warningAmount;
        private int idle;
        private LadderInfo ladder;

        public string WatchType
        {
            get
            {
                return watchType;
            }

            set
            {
                watchType = value;
            }
        }

        public string CardType
        {
            get
            {
                return cardType;
            }

            set
            {
                cardType = value;
            }
        }

        public string CardNo
        {
            get
            {
                return cardNo;
            }

            set
            {
                cardNo = value;
            }
        }

        public int OrderAmount
        {
            get
            {
                return orderAmount;
            }

            set
            {
                orderAmount = value;
            }
        }

        public int OrderNo
        {
            get
            {
                return orderNo;
            }

            set
            {
                orderNo = value;
            }
        }

        public int WatchLimit
        {
            get
            {
                return watchLimit;
            }

            set
            {
                watchLimit = value;
            }
        }

        public int OverdraftAmount
        {
            get
            {
                return overdraftAmount;
            }

            set
            {
                overdraftAmount = value;
            }
        }

        public int WarningAmount
        {
            get
            {
                return warningAmount;
            }

            set
            {
                warningAmount = value;
            }
        }

        public int Idle
        {
            get
            {
                return idle;
            }

            set
            {
                idle = value;
            }
        }

        public LadderInfo Ladder
        {
            get
            {
                return ladder;
            }

            set
            {
                ladder = value;
            }
        }

        // deserialize
        public void fill(string paramStr, string ladderStr)
        {
            parseParamStr(paramStr);
            if (this.ladder == null)
                this.Ladder = new ZJWXLadderInfo();
            this.Ladder.parseLadderString(ladderStr);
        }

        public bool parseParamStr(string paramStr)
        {
            char[] sp = { '|' };
            string[] paramArr = paramStr.Split(sp);

            int _orderAmount = 0;
            int _orderNo = 0;
            int _watchLimit = 0;
            int _overdraftAmount = 0;
            int _warningAmount = 0;
            int _idle = 0;

            this.WatchType = paramArr[0];
            this.CardType = paramArr[1];
            this.CardNo = paramArr[2];
            int.TryParse(paramArr[3], out _orderAmount);
            this.OrderAmount = _orderAmount;
            int.TryParse(paramArr[4], out _orderNo);
            this.OrderNo = _orderNo;
            int.TryParse(paramArr[5], out _watchLimit);
            this.WatchLimit = _watchLimit;
            int.TryParse(paramArr[6], out _overdraftAmount);
            this.OverdraftAmount = _overdraftAmount;
            int.TryParse(paramArr[7], out _warningAmount);
            this.WarningAmount = _warningAmount;
            int.TryParse(paramArr[8], out _idle);
            this.Idle = _idle;

            return true;
        }

        // serialize
        public string getParamString()
        {
            string paramStr = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",
                this.WatchType,
                this.CardType,
                this.CardNo,
                this.OrderAmount,
                this.OrderNo,
                this.WatchLimit,
                this.OverdraftAmount,
                this.WarningAmount,
                this.Idle
                );
            
            return paramStr;
        }

        public string getLadderString()
        {
            return this.Ladder.getLadderString();
        }

        public override string Serialize()
        {
            string ret = "{";
            ret += string.Format("\"watchType\":{0}," +
                "\"cardType\":{1}," +
                "\"cardNo\":\"{2}\"," +
                "\"orderAmount\":{3}," +
                "\"orderNo\":{4}," +
                "\"watchLimit\":{5}," +
                "\"overdraftAmount\":{6}," +
                "\"warningAmount\":{7}," +
                "\"idle\":{8}," +
                "\"ladder\":{9}",
                this.WatchType,
                this.CardType,
                this.CardNo,
                this.OrderAmount,
                this.OrderNo,
                this.WatchLimit,
                this.OverdraftAmount,
                this.WarningAmount,
                this.Idle,
                this.Ladder.Serialize()
                );
            ret += "}";

            return ret;
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class ZJWXOrderInfo : OrderInfo
    {
        private string pamaInfo;
        private string ladderInfo;

        public string PamaInfo
        {
            get
            {
                return pamaInfo;
            }

            set
            {
                pamaInfo = value;
            }
        }

        public string LadderInfo
        {
            get
            {
                return ladderInfo;
            }

            set
            {
                ladderInfo = value;
            }
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            bool ret = false;

            // get order params from request
            if (request.QueryString["PamaInfo"] != null && request.QueryString["LadderInfo"] != null)
            {
                this.PamaInfo = request.QueryString["PamaInfo"];
                this.LadderInfo = request.QueryString["LadderInfo"];
                ret = true;
            }

            return ret;
        }

        public override string Serialize()
        {
            throw new NotImplementedException();
        }
    }

    public class ZJWXCardMetaInfo : CardMetaInfo
    {
        private int meterType;
        private int cardType;
        private byte[] cardNo;

        public int MeterType
        {
            get
            {
                return meterType;
            }

            set
            {
                meterType = value;
            }
        }

        public int CardType
        {
            get
            {
                return cardType;
            }

            set
            {
                cardType = value;
            }
        }

        public byte[] CardNo
        {
            get
            {
                return cardNo;
            }

            set
            {
                cardNo = value;
            }
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            bool ret = false;
            int mt;
            int ct;

            // get order params from request
            if (request.QueryString["MeterType"] != null && request.QueryString["CardType"] != null && request.QueryString["CardNo"] != null)
            {
                if (int.TryParse(request.QueryString["MeterType"], out mt) == true)
                    this.MeterType = mt;
                else
                    return ret;

                if (int.TryParse(request.QueryString["CardType"], out ct) == true)
                    this.CardType = ct;
                else
                    return ret;

                this.CardNo = Encoding.Default.GetBytes(request.QueryString["CardNo"]);

                ret = true;
            }

            return ret;
        }

        public override string Serialize()
        {
            throw new NotImplementedException();
        }
    }

    public class ZJWXCardReader : CardReaderAdpator
    {
        // DLL imports
        [DllImportAttribute("ZJWXGas.dll", EntryPoint = "ZJWX_GasInitPort", CallingConvention = CallingConvention.StdCall)]
        private static extern int ZJWX_GasInitPort(int port, int baud, byte[] results);

        [DllImportAttribute("ZJWXGas.dll", EntryPoint = "ZJWX_GasReadCardInfo", CallingConvention = CallingConvention.StdCall)]
        private static extern int ZJWX_GasReadCardInfo(int lcdev, byte[] PamaInfo, byte[] LadderInfo, byte[] results);

        [DllImportAttribute("ZJWXGas.dll", EntryPoint = "ZJWX_GasWriteCardInfo", CallingConvention = CallingConvention.StdCall)]
        private static extern int ZJWX_GasWriteCardInfo(int lcdev, byte[] PamaInfo, byte[] LadderInfo, byte[] results);

        [DllImportAttribute("ZJWXGas.dll", EntryPoint = "ZJWX_GasMakeCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int ZJWX_GasMakeCard(int lcdev, int MeterType, int CardType, byte[] CardNo, byte[] results);

        [DllImportAttribute("ZJWXGas.dll", EntryPoint = "ZJWX_GasClearCard", CallingConvention = CallingConvention.StdCall)]
        private static extern int ZJWX_GasClearCard(int lcdev, byte[] results);

        [DllImportAttribute("ZJWXGas.dll", EntryPoint = "ZJWX_GasExitPort", CallingConvention = CallingConvention.StdCall)]
        private static extern int ZJWX_GasExitPort(int lcdev);

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

        public ZJWXCardReader()
        {
            this.type = DeviceType.Printer;
            this.vendor = "ZJWX";
        }

        // helper
        public int getDevNo(string resultStr)
        {
            string devNoStr = resultStr.Substring(resultStr.LastIndexOf(':') + 1);
            int devNo = int.Parse(devNoStr);

            return devNo;
        }

        // interface
        public override CardReaderResponseCode CheckReader(int port, int baudrate)
        {
            throw new NotImplementedException();
        }

        public override CardInfo ReadCard()
        {
            ZJWXCardInfo cardInfo = new ZJWXCardInfo();

            // read card and fill card info
            byte[] PamaInfo = new byte[255];
            byte[] LadderInfo = new byte[255];
            byte[] results = new byte[255];

            // open port
            int ret = ZJWX_GasInitPort((int)this.Port, (int)this.Baudrate, results);
            if (ret == -1)
            {
                return null;
            }

            string resultsStr = System.Text.Encoding.Default.GetString(results).Trim('\0');
            int dev = getDevNo(resultsStr);

            // read card
            ret = ZJWX_GasReadCardInfo(dev, PamaInfo, LadderInfo, results);
            if (ret == -1)
            {
                return null;
            }

            string PamaInfoStr = System.Text.Encoding.Default.GetString(PamaInfo).Trim('\0');
            string LadderInfoStr = System.Text.Encoding.Default.GetString(LadderInfo).Trim('\0');
            resultsStr = System.Text.Encoding.Default.GetString(results).Trim('\0');

            // EventLog.WriteEntry(ConfigurationManager.AppSettings["LogSource"], "PamaInfoStr: " + PamaInfoStr + "\nLadderInfoStr: " + LadderInfoStr, EventLogEntryType.Information);
            cardInfo.fill(PamaInfoStr, LadderInfoStr);

            // close port
            ret = ZJWX_GasExitPort(dev);

            return cardInfo;
        }

        public override CardReaderResponseCode WriteCard(OrderInfo order)
        {
            byte[] results = new byte[255];

            // open port
            int ret = ZJWX_GasInitPort((int)this.Port, (int)this.Baudrate, results);
            if (ret == -1)
            {
                return CardReaderResponseCode.CommError;
            }

            string resultsStr = System.Text.Encoding.Default.GetString(results).Trim('\0');
            int dev = getDevNo(resultsStr);

            // write card
            ret = ZJWX_GasWriteCardInfo(dev, Encoding.Default.GetBytes(((ZJWXOrderInfo)order).PamaInfo), Encoding.Default.GetBytes(((ZJWXOrderInfo)order).LadderInfo), results);
            if (ret == -1)
            {
                return CardReaderResponseCode.WriteError;
            }

            // close port
            ret = ZJWX_GasExitPort(dev);
            // we don't care if there's closing error since writing is already done

            return CardReaderResponseCode.Success;
        }

        public override CardReaderResponseCode MakeCard(CardMetaInfo metaInfo)
        {
            byte[] results = new byte[255];

            // open port
            int ret = ZJWX_GasInitPort((int)this.Port, (int)this.Baudrate, results);
            if (ret == -1)
            {
                return CardReaderResponseCode.CommError;
            }

            string resultsStr = System.Text.Encoding.Default.GetString(results).Trim('\0');
            int dev = getDevNo(resultsStr);

            // make card
            ret = ZJWX_GasMakeCard(dev, ((ZJWXCardMetaInfo)metaInfo).MeterType, ((ZJWXCardMetaInfo)metaInfo).CardType, ((ZJWXCardMetaInfo)metaInfo).CardNo, results);
            if (ret == -1)
            {
                return CardReaderResponseCode.CardError;
            }

            // close port
            ret = ZJWX_GasExitPort(dev);
            // we don't care if there's closing error since writing is already done

            return CardReaderResponseCode.Success;
        }

        public override CardReaderResponseCode ClearCard()
        {
            throw new NotImplementedException();
        }

        public override WatchInfo ReadWatchInfo()
        {
            // ZJWX doesn't support this function
            throw new NotImplementedException();
        }
    }
}
