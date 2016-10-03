﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContraloriaNDSWeb.Models
{
    public class MisionVision
    {
        [Key]
        public int MisionVisionId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(1, double.MaxValue, ErrorMessage = "You must be select a {0}")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(4000, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Misión")]
        [DataType(DataType.MultilineText)]
        public string Mision { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(4000, ErrorMessage = "El campo {0} debe ser máximo de {1} caracteres")]
        [Display(Name = "Visión")]
        [DataType(DataType.MultilineText)]
        public string Vision { get; set; }

        

        public virtual Company Company { get; set; }
    }
}