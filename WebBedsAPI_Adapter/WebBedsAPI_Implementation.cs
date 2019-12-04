using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebBedsAPI_Adapter
{
    public class WebBedsAPI_Implementation : IWebBedsAPI
    {
        private string _apiURI;
        public WebBedsAPI_Implementation(string apiURI)
        {
            _apiURI = apiURI;
        }
        public async Task<FindBargainResponseMessage> FindBargain(string apiMethodName, int destinationId, int nights, string authCode)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(_apiURI);
                    var requestUri = string.Format(apiMethodName + "?destinationId={0}&nights={1}&code={2}", destinationId, nights, authCode);
                    using (var response = await client.GetAsync(requestUri))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonString = await response.Content.ReadAsStringAsync();

                            var result = JsonConvert.DeserializeObject<List<FindBargainAPIJsonObject>>(jsonString);

                            FindBargainResponseMessage responseMessage = new FindBargainResponseMessage();
                            if (result != null && result.Count > 0)
                            {
                                foreach (var hotel in result)
                                {
                                    foreach (var rate in hotel.rates)
                                    {
                                        responseMessage.HotelDetails.Add(new HotelDetails
                                        {
                                            BoardType = rate.boardType,
                                            HotelName = hotel.hotel.name,
                                            BasePrice = rate.value,
                                            ActualPrice = rate.rateType == "PerNight" ? rate.value * nights : rate.value,
                                            RateType = rate.rateType
                                        });
                                    }
                                }
                            }

                            return responseMessage;
                        }
                        else
                        {
                            throw new Exception("API Exception Occurred. Try with different query or contact support.");
                        }
                    }
                }
                catch { throw; }
            }
        }
    }
}