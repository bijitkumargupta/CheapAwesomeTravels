using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebBeds_Bijit.Models;
using WebBedsAPI_Adapter;

namespace WebBeds_Bijit.Controllers
{
    public class HomeController : Controller
    {
        private IWebBedsAPI _webBedsApiHandler;
        private readonly IConfiguration _config;
        private readonly IConfigurationSection _appSettings;
        public HomeController(IConfiguration config)
        {
            _config = config;
            _appSettings = _config.GetSection("AppSettings");
            _webBedsApiHandler = new WebBedsAPI_Implementation(_appSettings["WebBedsAPI_URL"]);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(HotelSearchRequest searchModel)
        {
            try
            {
                var authCode = _appSettings["WebBedsAPI_AuthCode"];
                var apiMethodName = _appSettings["WebBedsAPI_FindBargain_MethodName"];
                ViewData["ResultSet"] = await _webBedsApiHandler.FindBargain(apiMethodName, searchModel.DestinationId, searchModel.Nights, authCode);

                return View();
            }
            catch(Exception ex)
            {
                return View("../Views/Shared/Error.cshtml", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ExceptionMessage = ex.Message });
            }
        }
    }
}
