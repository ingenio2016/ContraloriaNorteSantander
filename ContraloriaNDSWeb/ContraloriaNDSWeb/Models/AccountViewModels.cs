using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContraloriaNDSWeb.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Correo")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "¿Recordar en este Navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Correo")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Correo")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "La {0} deben tener al menos una letra o carácter no dígitos . La {0} deben tener al menos una mayúscula ( A - Z ).")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordarme?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe ser de máximo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "La {0} deben tener al menos una letra o carácter no dígitos . La {0} deben tener al menos una mayúscula ( A - Z ).")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "La {0} deben tener al menos una letra o carácter no dígitos . La {0} deben tener al menos una mayúscula ( A - Z ).")]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe ser de máximo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password, ErrorMessage = "La {0} deben tener al menos una letra o carácter no dígitos . La {0} deben tener al menos una mayúscula ( A - Z ).")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password, ErrorMessage = "La {0} deben tener al menos una letra o carácter no dígitos . La {0} deben tener al menos una mayúscula ( A - Z ).")]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }
    }
}
