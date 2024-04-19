using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Silicon_WebApp.ViewModels
{
    public class SignInViewModel
    {
        [Required]
       
        [Display(Name = "E-mail adress", Prompt="Enter your email adress")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }=null!;

        [Required]

        [Display(Name = "Password", Prompt = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;

        [Display(Name = "Remember me", Prompt = "Remember me")]
        public bool IsPersistent { get; set; }
    }
}
