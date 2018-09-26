using System.ComponentModel.DataAnnotations;
namespace WebAppFullFramework.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual:", Order = 1)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Minimo 6 caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha:", Order = 2)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Repita nova senha:", Order = 3)]
        [Compare("NewPassword", ErrorMessage = "Minimo 6 caracteres")]
        public string ConfirmPassword { get; set; }
    }
}