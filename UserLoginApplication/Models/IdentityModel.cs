using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace UserLoginApplication.Models
{
    public class IdentityModel
    {
        [DisplayName("Username")]
        [Required]
        public string UserName { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        public string Email { get; set; } = string.Empty;
        public long? PhoneNumber { get; set; }
        public int? EmployeeID { get; set; }
        public string EmployeeState { get; set; } = string.Empty;
    }
}