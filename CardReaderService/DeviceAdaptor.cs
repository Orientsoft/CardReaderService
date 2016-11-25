using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CardReaderService
{
    public interface HttpSerializable
    {
        // convert to jsonp string
        string Serialize();

        // read from respsonse context
        bool Deserialize(HttpListenerRequest request);
    }

    // Device
    public enum DeviceType
    {
        CardReader,
        Printer
    }

    public abstract class DeviceAdaptor
    {
        public abstract string Id { get; }
        public abstract DeviceType Type { get; }
        public abstract string Vendor { get; }
    }

    // CardReader
    public enum CardReaderResponseCode
    {
        Success,
        ReadError,
        WriteError,
        WrongUser,
        WrongPassword,
        CardNotFound,
        CardDisposed,
        CardUsed,
        CardPulledOut,
        ValueOutOfRange,
        ValueOutOfLength,
        CommError,
        CardError,
        SerialInUse,
        AmoutNotUsed,
        AmoutError
    }

    public abstract class LadderInfo : HttpSerializable
    {
        public abstract int PriceNo { get; set; }
        public abstract string ExecDate { get; set; }
        public abstract string StartoverDate { get; set; }
        public abstract int[] Price { get; set; }
        public abstract int[] Vol { get; set; }

        public abstract string getLadderString();
        public abstract bool parseLadderString(string ladderStr);

        public abstract string Serialize();
        public abstract bool Deserialize(HttpListenerRequest request);
    }

    public abstract class CardInfo : HttpSerializable
    {
        public abstract string Serialize();
        public abstract bool Deserialize(HttpListenerRequest request);
    }

    public abstract class CardMetaInfo : HttpSerializable
    {
        public abstract string Serialize();
        public abstract bool Deserialize(HttpListenerRequest request);
    }

    public abstract class OrderInfo : HttpSerializable
    {
        public abstract string Serialize();
        public abstract bool Deserialize(HttpListenerRequest request);
    }

    public abstract class WatchInfo : HttpSerializable
    {
        public abstract string Serialize();
        public abstract bool Deserialize(HttpListenerRequest request);
    }

    public abstract class CardReaderAdpator : DeviceAdaptor
    {
        public abstract int Port { get; set; }
        public abstract int Baudrate { get; set; }

        public abstract CardReaderResponseCode CheckReader(int port, int baudrate);
        public abstract CardInfo ReadCard();
        public abstract CardReaderResponseCode WriteCard(OrderInfo order);
        public abstract CardReaderResponseCode MakeCard(CardMetaInfo metaInfo);
        public abstract CardReaderResponseCode ClearCard();
        public abstract WatchInfo ReadWatchInfo();
    }
}
