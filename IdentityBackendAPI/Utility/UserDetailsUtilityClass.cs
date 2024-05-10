using IdentityBackendAPI.Models;

namespace IdentityBackendAPI.Utility
{
    public class UserDetailsUtilityClass
    {
        List<IdentityModel> userDetails = new List<IdentityModel>();
        public UserDetailsUtilityClass()
        { 
            userDetails.Add(new IdentityModel { UserName = "admin", Password = "admin", Email = "admin@example.com", EmployeeID = 33333, EmployeeState = "Active", PhoneNumber = 5849393439 });
            userDetails.Add(new IdentityModel { UserName = "ananthu", Password = "ananthu", Email = "ananthu@example.com", EmployeeID = 12345, EmployeeState = "Active", PhoneNumber = 9876543210 });
            userDetails.Add(new IdentityModel { UserName = "rahul", Password = "rahul", Email = "rahul@example.com", EmployeeID = 54321, EmployeeState = "Active", PhoneNumber = 1234567890 });
            userDetails.Add(new IdentityModel { UserName = "john", Password = "john", Email = "john@example.com", EmployeeID = 67890, EmployeeState = "Inactive", PhoneNumber = 5678901234 });
            userDetails.Add(new IdentityModel { UserName = "rama", Password = "rama", Email = "rama@example.com", EmployeeID = 98765, EmployeeState = "Active", PhoneNumber = 8901234567 });
        }
        public List<IdentityModel> GetUserDetails()
        {
            return userDetails;
        }
    }
}
