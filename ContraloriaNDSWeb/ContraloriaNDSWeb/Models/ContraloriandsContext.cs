using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class ContraloriandsContext : DbContext
    {
        public ContraloriandsContext() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Company> Companies { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.MisionVision> MisionVisions { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Objetivo> Objetivoes { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.PlanesProgramas> PlanesProgramas { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.PrincipiosEticos> PrincipiosEticos { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.ValoresEticos> ValoresEticos { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.PoliticasCalidad> PoliticasCalidads { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Compromiso> Compromisoes { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Directorio> Directorios { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Presupuesto> Presupuestoes { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Year> Years { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.PlanesAccion> PlanesAccions { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.PlanAnticorrupcion> PlanAnticorrupcions { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Contralor> Contralors { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.RecursoHumano> RecursoHumanoes { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Entes> Entes { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Consejo> Consejoes { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Hospital> Hospitals { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Personeria> Personerias { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.InformeGestion> InformeGestions { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.ControlInterno> ControlInternoes { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.FuncionAdvertencia> FuncionAdvertencias { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.BeneficioControlFiscal> BeneficioControlFiscals { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.JurisdiccionCoactiva> JurisdiccionCoactivas { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.ControlAmbiental> ControlAmbientals { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Auditoria> Auditorias { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.AuditoriaInterna> AuditoriaInternas { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.AuditoriaExterna> AuditoriaExternas { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.Notificacion> Notificacions { get; set; }

        public System.Data.Entity.DbSet<ContraloriaNDSWeb.Models.DenunciasCategorias> DenunciasCategorias { get; set; }
    }
}