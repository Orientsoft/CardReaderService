using System;
using System.Collections.Generic;
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

            this.PriceNo = int.Parse(ladderArr[0]);
            this.ExecDate = ladderArr[1];
            this.StartoverDate = ladderArr[2];

            for (int i = 0; i < (ladderArr.Length - 3) / 2; i++)
            {
                this.Price[i] = int.Parse(ladderArr[i * 2 + 3]);
                this.Vol[i] = int.Parse(ladderArr[i * 2 + 4]);
            }

            return true;
        }

        public override string Serialize()
        {
            string json = "LadderInfo:{";

            json += "\"priceNo\":\"" + this.PriceNo + "\"," +
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
            this.Ladder.parseLadderString(ladderStr);
        }

        public bool parseParamStr(string paramStr)
        {
            char[] sp = { '|' };
            string[] paramArr = paramStr.Split(sp);

            this.WatchType = paramArr[0];
            this.CardType = paramArr[1];
            this.CardNo = paramArr[2];
            this.OrderAmount = int.Parse(paramArr[3]);
            this.OrderNo = int.Parse(paramArr[4]);
            this.WatchLimit = int.Parse(paramArr[5]);
            this.OverdraftAmount = int.Parse(paramArr[6]);
            this.WarningAmount = int.Parse(paramArr[7]);
            this.Idle = int.Parse(paramArr[8]);

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
            throw new NotImplementedException();
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            throw new NotImplementedException();
        }
    }

    class ZJWXCardReader : CardReaderAdpator
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
            string resultsStr = System.Text.Encoding.Default.GetString(results).Trim('\0'); ;
            int dev = getDevNo(resultsStr);

            // read card
            ret = ZJWX_GasReadCardInfo(dev, PamaInfo, LadderInfo, results);

            string PamaInfoStr = System.Text.Encoding.Default.GetString(PamaInfo).Trim('\0');
            string LadderInfoStr = System.Text.Encoding.Default.GetString(LadderInfo).Trim('\0');
            resultsStr = System.Text.Encoding.Default.GetString(results).Trim('\0');

            cardInfo.fill(PamaInfoStr, LadderInfoStr);

            // close port
            ret = ZJWX_GasExitPort(dev);

            return cardInfo;
        }

        public override CardReaderResponseCode WriteCard(OrderInfo order)
        {
            throw new NotImplementedException();
        }

        public override CardReaderResponseCode MakeCard(CardMetaInfo metaInfo)
        {
            throw new NotImplementedException();
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
