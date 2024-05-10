using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UserLoginApplication.Abstraction;
using UserLoginApplication.Models;
using UserLoginApplication.Utility;

namespace UserLoginApplication.Controllers
{
    public class HomeController : Controller
    {
        private string baseUrl = WebConfigurationManager.AppSettings["baseURL"];
        IHttpUtility _httpUtility;
       
        [HttpGet]
        [Route("Index")]
        public async Task<ActionResult> Index()
        {
            baseUrl = $"{baseUrl}/api/GetAllUserDetails";
            ViewBag.Title = "Dashboard";

            _httpUtility = new HttpUtilityClass(baseUrl, TempData["AuthKey"].ToString());

            List<IdentityModel> userDets = await _httpUtility.UserDetails();

            return View(userDets);
        }
    }
}