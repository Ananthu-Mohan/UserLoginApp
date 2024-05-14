using IdentityBackendAPI.Models;
using IdentityBackendAPI.ResponseMessages;
using IdentityBackendAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;

namespace IdentityBackendAPI.Controllers
{
    [ApiController]
    public class UserAuthenticationController : ControllerBase
    {
        IConfiguration _configuration;
        List<IdentityModel> userDetails = new List<IdentityModel>();
        UserDetailsUtilityClass userDetailObj = new UserDetailsUtilityClass();
        public UserAuthenticationController(IConfiguration configuration)
        {
             userDetails = userDetailObj.GetUserDetails();
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/AuthenticateUser")]
        public async Task<ResponseMessage> AuthenticateUser([FromBody] IdentityModel userDetailsContent)
        {
            string content = string.Empty;

            ResponseMessage resAuth = new ResponseMessage();
            TokenUtility tokenUtility = new TokenUtility(_configuration);

            if (userDetailsContent == null)
            {
                resAuth.Status = false;
                resAuth.Message = $"Invalid Username and Password";
            }
            else
            {
                IdentityModel userFound = userDetails.Where(user => user.Email == userDetailsContent.Email && user.Password == userDetailsContent.Password && String.Equals(user.EmployeeState, "Active")).FirstOrDefault();
                if (userFound != null)
                {
                    resAuth.Status = true;
                    resAuth.Message = $"User - {userDetailsContent.UserName} authenticated";
                    (resAuth.apiKeyExpiration, resAuth.apiKey) = tokenUtility.GetToken(userDetailsContent);
                    resAuth.userDets = userFound;
                }
                else
                {
                    resAuth.Status = false;
                    resAuth.Message = $"Invalid Username and Password / Inactive User";
                }
            }
            return resAuth;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("api/TokenKeyForCloudUsers")]
        public async Task<ResponseMessage> TokenKeyForCloudUsers([FromBody] string username)
        {
            ResponseMessage resAuth = new ResponseMessage();
            TokenUtility tokenUtility = new TokenUtility(_configuration);

            if(!String.IsNullOrEmpty(username))
            {
                resAuth.Status = true;
                resAuth.Message = $"User - {username} authenticated";
                (resAuth.apiKeyExpiration, resAuth.apiKey) = tokenUtility.GetTokenForJumpCloudUsers(username);
            }
            return resAuth;
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetAllUserDetails")]
        public async Task<List<IdentityModel>> GetAllUserDetails()
        {
            return userDetails;
        }
    }
}
