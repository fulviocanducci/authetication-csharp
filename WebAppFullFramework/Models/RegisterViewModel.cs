//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebAppFullFramework.Models
{
    
    public class RegisterViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail:")]
        //[Remote("", "Id", ErrorMessage = "Email já existente")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Digite a senha com 6 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repetir Senha:")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Senha e repetir senha não são iguais.")]
        public string ConfirmPassword { get; set; }        
    }    
}
