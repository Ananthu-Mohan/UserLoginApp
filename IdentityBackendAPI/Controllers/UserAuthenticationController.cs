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
                if (userDetails.Where(user => user.UserName == userDetailsContent.UserName && user.Password == userDetailsContent.Password).Count() > 0)
                {
                    resAuth.Status = true;
                    resAuth.Message = $"User - {userDetailsContent.UserName} authenticated";
                    resAuth.apiKey = tokenUtility.GetToken(userDetailsContent);
                }
                else
                {
                    resAuth.Status = false;
                    resAuth.Message = $"Invalid Username and Password";
                }
            }
            return resAuth;
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetAllUserDetails")]
        public IQueryable<IdentityModel> GetAllUserDetails()
        {
            return userDetails.AsQueryable();
        }
    }
}
