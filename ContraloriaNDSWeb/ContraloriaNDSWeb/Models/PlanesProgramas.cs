﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class PlanesProgramas
    {
        [Key]
        public int PlanesProgramasId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must be select a {0}")]
        [Display(Name = "Company")]
       
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(2000, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Descripción del Plan de Programa")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [MaxLength(300, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Documento Adjunto")]
        //[Index("MisionVision_CompanyId_Mision_Index", 2, IsUnique = true)]
        public string Adjunto { get; set; }

        [NotMapped]
        [Display(Name = "Documento Adjunto")]
        public HttpPostedFileBase AdjuntoFile { get; set; }

        public virtual Company Company { get; set; }
    }
}