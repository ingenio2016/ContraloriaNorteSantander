using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class Contralor
    {
        [Key]
        public int ContralorId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Apelllido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(2000, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Reseña")]
        [DataType(DataType.MultilineText)]
        public string Resena { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Contraloría")]
        public int CompanyId { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

        [DataType(DataType.ImageUrl)]
        public string Photo { get; set; }

        [NotMapped]
        public HttpPostedFileBase PhotoFile { get; set; }

        public virtual Company Company { get; set; }
    }
}