using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Contraloria")]
        [Index("Company_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Departamento")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Ciudad")]
        public int CityId { get; set; }


        public virtual Department Department { get; set; }

        public virtual City City { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<MisionVision> MisionsVisions { get; set; }

        public virtual ICollection<Objetivo> Objetivos { get; set; }

        public virtual ICollection<PlanesProgramas> PlanesProgramas { get; set; }

        public virtual ICollection<PrincipiosEticos> PrincipiosEticos { get; set; }

        public virtual ICollection<ValoresEticos> ValoresEticos { get; set; }

        public virtual ICollection<PoliticasCalidad> PoliticasCalidad { get; set; }

        public virtual ICollection<Directorio> Directorio { get; set; }
        public virtual ICollection<Presupuesto> Presupuesto { get; set; }

        public virtual ICollection<Year> Years { get; set; }

        public virtual ICollection<PlanesAccion> PlanesAccion { get; set; }

        public virtual ICollection<PlanAnticorrupcion> PlanAnticorrupcion { get; set; }

        public virtual ICollection<RecursoHumano> RecursoHumano { get; set; }

        public virtual ICollection<Entes> Entes { get; set; }

        public virtual ICollection<Consejo> Consejo { get; set; }

        public virtual ICollection<Hospital> Hospital { get; set; }

        public virtual ICollection<Personeria> Personeria { get; set; }

        public virtual ICollection<InformeGestion> InformeGestion { get; set; }

        public virtual ICollection<ControlInterno> ControlInterno { get; set; }

        public virtual ICollection<FuncionAdvertencia> FuncionAdvertencia { get; set; }

        public virtual ICollection<BeneficioControlFiscal> BeneficioControlFiscal { get; set; }

        public virtual ICollection<JurisdiccionCoactiva> JurisdiccionCoactiva { get; set; }

        public virtual ICollection<ControlAmbiental> ControlAmbiental { get; set; }

        public virtual ICollection<Auditoria> Auditoria { get; set; }

        public virtual ICollection<AuditoriaInterna> AuditoriaInterna { get; set; }

        public virtual ICollection<AuditoriaExterna> AuditoriaExterna { get; set; }

        public virtual ICollection<Notificacion> Notificacion { get; set; }

        public virtual ICollection<DenunciasCategorias> DenunciasCategorias { get; set; }


    }
}