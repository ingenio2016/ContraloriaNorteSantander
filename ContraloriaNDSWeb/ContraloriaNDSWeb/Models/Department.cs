using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Departamento")]
        [Index("Department_Name_Index", IsUnique = true)]
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; } //Relacion de uno a muchos este es el Mucho

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}