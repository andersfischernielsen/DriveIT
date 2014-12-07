using System.ComponentModel.DataAnnotations;

namespace DriveIT.Models
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}