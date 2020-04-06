using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PFinalWeb.Models
{
    public class PNoticia
    {
        public int IDNew { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public System.DateTime Fecha { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Resumen { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Upload)]
        public byte[] Foto { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Contenido { get; set; }
    }

    [MetadataType(typeof(PNoticia))]
    public partial class Noticias {
        public HttpPostedFileBase file { get; set; }
    }
}