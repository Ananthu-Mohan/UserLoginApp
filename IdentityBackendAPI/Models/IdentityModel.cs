namespace IdentityBackendAPI.Models
{
    public class IdentityModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public long? PhoneNumber { get; set; }
        public int? EmployeeID { get; set; }
        public string? EmployeeState { get; set; }
    }
}
