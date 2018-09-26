using System.ComponentModel.DataAnnotations;
namespace WebAppFullFramework.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "E-mail", Order = 1)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha", Order = 2)]
        public string Password { get; set; }

        [Display(Name = "Lembrar?", Order = 3)]
        public bool RememberMe { get; set; }
    }
}