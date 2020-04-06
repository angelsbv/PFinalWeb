using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PFinalWeb.Models
{
    public class PCaso
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [MinLength(11, ErrorMessage = "Debe tener 11 digitos."), 
            MaxLength(11, ErrorMessage = "Debe tener 11 digitos.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string País { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public decimal Latitud { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public decimal Longitud { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DataType(DataType.Date)]
        [CurrentDate(ErrorMessage = "La fecha debe ser menor o igual que la actual.")]
        public System.DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DataType(DataType.Date)]
        [CurrentDate(ErrorMessage = "La fecha debe ser menor o igual que la actual.")]
        public System.DateTime FechaContagio { get; set; }
    }

    [MetadataType(typeof(PCaso))]
    public partial class Casos
    {

    }

    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if (dt <= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}