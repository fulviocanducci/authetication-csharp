using System.ComponentModel.DataAnnotations;

namespace WebAppFullFramework.Models
{
    public class ForgotViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}