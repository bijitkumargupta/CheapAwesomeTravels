using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace WebBedsAPI_Adapter.Test
{
    [TestClass]
    public class TestFairBargainAPI
    {
        private IWebBedsAPI _webBedsApiHandler;
        private readonly string _authCode = "aWH1EX7ladA8C/oWJX5nVLoEa4XKz2a64yaWVvzioNYcEo8Le8caJw==";
        private readonly string _apiUrl = "https://webbedsdevtest.azurewebsites.net/api/";
        private readonly string _apiMethodName = "findBargain";
        public TestFairBargainAPI()
        {
            _webBedsApiHandler = new WebBedsAPI_Implementation(_apiUrl);
        }
        [TestMethod]
        public async void Test_OnlyAPICall()
        {
            try
            {
                int destinationId = 279;
                int nightCount = 2;
                await _webBedsApiHandler.FindBargain(_apiMethodName, destinationId, nightCount, _authCode);
            }
            catch (System.Exception)
            {
                Assert.Fail("API call returned error");
            }
        }

        [TestMethod]
        public async void Test_RateType_PerNight()
        {
            try
            {
                int destinationId = 279;
                int nightCount = 2;
                var result = await _webBedsApiHandler.FindBargain(_apiMethodName, destinationId, nightCount, _authCode);
                if (result.HotelDetails != null && result.HotelDetails.Any(h => h.RateType == "PerNight" && h.ActualPrice != h.BasePrice * nightCount))
                {
                    Assert.Fail("The Final Price calculated wrongly");
                }
            }
            catch (System.Exception)
            {
                Assert.Fail("API call returned error");
            }
        }

        [TestMethod]
        public async void Test_RateType_Others()
        {
            try
            {
                int destinationId = 279;
                int nightCount = 2;
                var result = await _webBedsApiHandler.FindBargain(_apiMethodName, destinationId, nightCount, _authCode);
                if (result.HotelDetails != null && result.HotelDetails.Any(h => h.RateType != "PerNight" && h.ActualPrice != h.BasePrice))
                {
                    Assert.Fail("The Final Price calculated wrongly");
                }
            }
            catch (System.Exception)
            {
                Assert.Fail("API call returned error");
            }
        }
    }
}