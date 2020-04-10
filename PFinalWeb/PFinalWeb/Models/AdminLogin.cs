using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PFinalWeb.Models
{
    public class AdminLogin
    {
        [Required(ErrorMessage = "Debe digitar su nombre de usuario.", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe digitar su contrase&ntilde;a.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}