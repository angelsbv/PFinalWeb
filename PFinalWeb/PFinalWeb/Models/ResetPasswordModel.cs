using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PFinalWeb.Models
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Debe digitar su nueva contrase&ntilde;a.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "La contrase&ntilde;a y la confirmaci&oacute;n no son iguales.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}