using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class PrincipiosEticos
    {
        [Key]
        public int PrincipiosEticosId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must be select a {0}")]
        [Display(Name = "Contraloría")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(1000, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Descripcion Principio")]
        public string Descripcion { get; set; }


        public virtual Company Company { get; set; }
    }
}