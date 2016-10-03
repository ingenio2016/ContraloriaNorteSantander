using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class ValoresEticos
    {
        [Key]
        public int ValoresEticosId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must be select a {0}")]
        [Display(Name = "Contraloría")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Nombre del Valor")]
        [Index("ValoresEticos_Nombre_Index", IsUnique = true)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(1000, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Descripcion del Valor")]
        public string Descripcion { get; set; }


        public virtual Company Company { get; set; }
    }
}