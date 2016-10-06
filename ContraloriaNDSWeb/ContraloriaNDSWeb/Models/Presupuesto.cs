using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class Presupuesto
    {
        [Key]
        public int PresupuestoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must be select a {0}")]
        [Display(Name = "Contraloría")]

        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(300, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Descripción del Presupuesto")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [MaxLength(300, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Documento Adjunto")]
        //[Index("MisionVision_CompanyId_Mision_Index", 2, IsUnique = true)]
        public string Adjunto { get; set; }

        [Display(Name = "Fecha Creación")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [MaxLength(300, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Creado por")]
        public string Autor { get; set; }

        [Display(Name = "Fecha Edición")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateEdition { get; set; }

        [MaxLength(300, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Editado por")]
        public string AutorEdition { get; set; }

        [NotMapped]
        [Display(Name = "Documento Adjunto")]
        public HttpPostedFileBase AdjuntoFile { get; set; }

        public virtual Company Company { get; set; }
    }
}