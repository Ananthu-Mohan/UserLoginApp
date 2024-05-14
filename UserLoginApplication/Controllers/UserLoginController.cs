using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UserLoginApplication.Abstraction;
using UserLoginApplication.Models;
using UserLoginApplication.ResponseMessages;
using UserLoginApplication.Utility;

namespace UserLoginApplication.Controllers
{
    [Route("UserLogin")]
    public class UserLoginController : Controller
    {
        private string baseUrl = WebConfigurationManager.AppSettings["baseURL"];
        IHttpUtility _httpUtility;

        [HttpGet]
        [Route("")]
        [Route("IdentityLogin")]
        public async Task<ActionResult> UserLogin()
        {
            if (Session["saml_sso_usernameusername"] != null)
            {
                baseUrl = $"{baseUrl}/api/TokenKeyForCloudUsers";
                bool ssoverification = Convert.ToBoolean(Session["saml_sso_verified"]);
                if (ssoverification)
                {
                    _httpUtility = new HttpUtilityClass(baseUrl);
                    var responseContent = await _httpUtility.TokenForJumpCloudUsers(Session["saml_sso_usernameusername"].ToString());
                    ResponseMessage responseMsg = JsonConvert.DeserializeObject<ResponseMessage>(responseContent);
                    Session["Username"] = Session["saml_sso_usernameusername"];
                    Session["AuthKey"] = responseMsg.apiKey;
                    Session["KeyExpiration"] = responseMsg.apiKeyExpiration.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        [Route("")]
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
                    Session["Username"] = responseMsg.userDets.UserName.ToString();
                    Session["AuthKey"] = responseMsg.apiKey;
                    Session["KeyExpiration"] = responseMsg.apiKeyExpiration.ToString();
                    if (string.Equals(responseMsg.userDets.UserName.ToString(), "admin"))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("ReadUser", "Home");
                    }
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