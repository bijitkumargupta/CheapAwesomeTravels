using System.ComponentModel.DataAnnotations;

namespace WebBeds_Bijit.Models
{
    public class HotelSearchRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Please select destination")]
        public int DestinationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a non-zero positve integer")]
        public int Nights { get; set; }
    }
}