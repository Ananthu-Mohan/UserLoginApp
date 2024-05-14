using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLoginApplication.Models;

namespace UserLoginApplication.Abstraction
{
    public interface IHttpUtility
    {
        Task<string> IsUserAuthenticated(IdentityModel userDetails);
        Task<List<IdentityModel>> UserDetails();
        Task<string> TokenForJumpCloudUsers(string username);
    }
}
