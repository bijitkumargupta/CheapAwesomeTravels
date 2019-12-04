using System.Collections.Generic;

namespace WebBedsAPI_Adapter
{
    public class FindBargainResponseMessage
    {
        public List<HotelDetails> HotelDetails { get; set; }
        public FindBargainResponseMessage()
        {
            HotelDetails = new List<HotelDetails>();
        }
    }

    public class HotelDetails
    {
        public string BoardType { get; set; }
        public string HotelName { get; set; }
        public string RateType { get; set; }
        public double BasePrice { get; set; }
        public double ActualPrice { get; set; }
    }
}
