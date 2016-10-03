using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Ciudad")]
        [Index("City_Name_Index", 2, IsUnique = true)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        [Display(Name = "Departamento")]
        [Index("City_Name_Index", 1, IsUnique = true)]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }//Relacion de unos a muchos, este es el uno

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}