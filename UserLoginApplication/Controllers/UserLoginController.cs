using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UserLoginApplication.Abstraction;
using UserLoginApplication.Models;
using UserLoginApplication.ResponseMessages;
using UserLoginApplication.Utility;

namespace UserLoginApplication.Controllers
{
    public class UserLoginController : Controller
    {
        private string baseUrl = "https://localhost:7145";
        IHttpUtility _httpUtility;

        [HttpGet]
        [Route("")]
        [Route("IdentityLogin")]
        public async Task<ActionResult> UserLogin()
        {
            return View();
        }
        [HttpPost]
        [Route("IdentityLogin")]
        public async Task<ActionResult> UserLogin(IdentityModel userLoginData)
        {
            if (ModelState.IsValid)
            {
                baseUrl = $"{baseUrl}/api/AuthenticateUser";

                _httpUtility = new HttpUtilityClass(baseUrl);
                var responseContent = await _httpUtility.IsUserAuthenticated(userLoginData);
                ResponseMessage responseMsg = JsonConvert.DeserializeObject<ResponseMessage>(responseContent);
                if (responseMsg.Status)
                {
                    var resToken = responseMsg.apiKey;
                    return Content(JsonConvert.SerializeObject(responseMsg));
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = responseMsg.Message;
                    return View(userLoginData);
                }
            }
            else
            {
                ViewBag.Message = $"User - {userLoginData.UserName} not Found";
                return View(userLoginData);
            }
        }
    }
}