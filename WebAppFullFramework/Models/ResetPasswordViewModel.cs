using System.ComponentModel.DataAnnotations;

namespace WebAppFullFramework.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail", Order = 1)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O valor minimo de caracteres é 6", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Order = 2)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password", Order = 3)]
        [Compare("Password", ErrorMessage = "A senha não é igual, verifique")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}