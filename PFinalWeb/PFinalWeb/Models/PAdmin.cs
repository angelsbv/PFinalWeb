using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PFinalWeb.Models
{
    public class PAdmin
    {
        public int UserID { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Debe digitar su nombre de usuario.")]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe digitar su correo electr&oacute;nico.")]
        [Display(Name = "Correo electr&oacute;nico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe digitar su fecha de nacimiento.")]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> Birthdate { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Contrase&ntilde;a")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "la contrase&ntilde;a debe ser m&iacute;nimo de 6 digitos.")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Confirmaci&oacute;n")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Las contrase&ntilde;as no coinciden. Vuelve a intentarlo.")]
        public string ConfirmPassword { get; set; }
    }

    [MetadataType(typeof(PAdmin))]
    public partial class Admins 
    {
        public string ConfirmPassword { get; set; }
    }
}