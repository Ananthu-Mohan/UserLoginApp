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
    }
}