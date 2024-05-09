using IdentityBackendAPI.Models;

namespace IdentityBackendAPI.Utility
{
    public class UserDetailsUtilityClass
    {
        List<IdentityModel> userDetails = new List<IdentityModel>();
        public UserDetailsUtilityClass()
        {
            userDetails.Add(new IdentityModel { UserName = "admin", Password = "admin" });
            userDetails.Add(new IdentityModel { UserName = "ananthu", Password = "ananthu" });
            userDetails.Add(new IdentityModel { UserName = "rahul", Password = "rahul" });
            userDetails.Add(new IdentityModel { UserName = "john", Password = "john" });
            userDetails.Add(new IdentityModel { UserName = "rama", Password = "rama" });
        }
        public List<IdentityModel> GetUserDetails()
        {
            return userDetails;
        }
    }
}
