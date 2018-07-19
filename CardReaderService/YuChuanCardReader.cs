using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CardReaderService
{
    public class YuChuanCardInfo : CardInfo
    {
        private string cardNo;
        private string customerNo;
        private string orderDate;
        private int orderCount;
        private int orderAmount;
        private int orderTotalAmount;
        private string branchNo;
        private string stationNo;
        private string key;

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

        public string CustomerNo
        {
            get
            {
                return customerNo;
            }

            set
            {
                customerNo = value;
            }
        }

        public string OrderDate
        {
            get
            {
                return orderDate;
            }

            set
            {
                orderDate = value;
            }
        }

        public int OrderCount
        {
            get
            {
                return orderCount;
            }

            set
            {
                orderCount = value;
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

        public int OrderTotalAmount
        {
            get
            {
                return orderTotalAmount;
            }

            set
            {
                orderTotalAmount = value;
            }
        }

        public string BranchNo
        {
            get
            {
                return branchNo;
            }

            set
            {
                branchNo = value;
            }
        }

        public string StationNo
        {
            get
            {
                return stationNo;
            }

            set
            {
                stationNo = value;
            }
        }

        public string Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }

        public override string Serialize()
        {
            string json = string.Format("\"CardNo\":\"{0}\"," +
                "\"CustomerNo\":\"{1}\"," +
                "\"OrderDate\":\"{2}\"," +
                "\"OrderCount\":\"{3}\"," +
                "\"OrderAmount\":\"{4}\"," +
                "\"OrderTotalAmount\":\"{5}\"," +
                "\"BranchNo\":\"{6}\"," +
                "\"StationNo\":\"{7}\"," +
                "\"Key\":\"{8}\",",
                cardNo,
                customerNo,
                orderDate,
                orderCount,
                orderAmount,
                orderTotalAmount,
                branchNo,
                stationNo);

            return json;
        }

        public override bool Deserialize(HttpListenerRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class YuChuanCardReader : CardReaderAdpator
    {
        public override int Baudrate
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override string Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int Port
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override DeviceType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string Vendor
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override CardReaderResponseCode CheckReader()
        {
            throw new NotImplementedException();
        }

        public override CardReaderResponseCode ClearCard()
        {
            throw new NotImplementedException();
        }

        public override CardReaderResponseCode MakeCard(CardMetaInfo metaInfo)
        {
            throw new NotImplementedException();
        }

        public override CardInfo ReadCard()
        {
            throw new NotImplementedException();
        }

        public override WatchInfo ReadWatchInfo()
        {
            throw new NotImplementedException();
        }

        public override CardReaderResponseCode WriteCard(OrderInfo order)
        {
            throw new NotImplementedException();
        }
    }
}
