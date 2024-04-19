using Silicon_WebApp.Filtres;
using System.ComponentModel.DataAnnotations;

namespace Silicon_WebApp.ViewModels
{
    public class SignUpViewModel
    {
        [Required]

        [Display(Name = "First name", Prompt = "Enter your first name")]
    
        public string FirstName { get; set; } = null!;
        [Required]

        [Display(Name = "Last name", Prompt = "Enter your last name")]
     
        public string LastName { get; set; } = null!;
        [Required]

        [Display(Name = "E-mail adress", Prompt = "Enter your email adress")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]

        [Display(Name = "Password", Prompt = "Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]

        [Display(Name = "Password", Prompt = "Enter your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = null!;

        [CheckboxRequired]
        [Display(Name = "I agree to the terms and conditions", Prompt="Terms and conditions")]
        public bool TermsAndConditions { get; set; }
    }
}
