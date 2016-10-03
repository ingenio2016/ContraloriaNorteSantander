using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class Compromiso
    {
        [Key]
        public int CompromisoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must be select a {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(2000, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Compromiso")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }


        public virtual Company Company { get; set; }
    }
}