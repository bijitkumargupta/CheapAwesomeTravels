using System.Collections.Generic;

namespace WebBedsAPI_Adapter
{
    public class FindBargainAPIJsonObject
    {
        public Hotel hotel { get; set; }
        public List<Rate> rates { get; set; }
    }
    public class Hotel
    {
        public int propertyID { get; set; }
        public string name { get; set; }
        public int geoId { get; set; }
        public int rating { get; set; }
    }
    public class Rate
    {
        public string rateType { get; set; }
        public string boardType { get; set; }
        public double value { get; set; }
    }
}
