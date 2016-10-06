using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class Year
    {
        [Key]
        public int YearId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must be select a {0}")]
        [Display(Name = "Contraloría")]
        public int CompanyId { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Año Laboral")]
        [Index("Year_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<PlanesAccion> PlanesAccion { get; set; }
        public virtual ICollection<PlanAnticorrupcion> PlanAnticorrupcion { get; set; }

        public virtual ICollection<FuncionAdvertencia> FuncionAdvertencia { get; set; }

        public virtual ICollection<Auditoria> Auditoria { get; set; }

        public virtual ICollection<AuditoriaInterna> AuditoriaInterna { get; set; }

        public virtual ICollection<AuditoriaExterna> AuditoriaExterna { get; set; }

        public virtual ICollection<Notificacion> Notificacion { get; set; }


    }
}