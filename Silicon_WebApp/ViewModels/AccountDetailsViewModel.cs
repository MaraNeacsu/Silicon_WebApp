using System.ComponentModel.DataAnnotations;

namespace Silicon_WebApp.ViewModels;

public class AccountDetailsViewModel
{
    public AccountBasicInfo?Basic { get; set; }
    public AccountAddressInfo? Address { get; set; }
  
}
public class AccountBasicInfo

{ 
  

    [Required]

    [Display(Name = "First name", Prompt = "Enter your first name")]

    public string FirstName { get; set; } = null!;
    [Required]

    [Display(Name = "Last name", Prompt = "Enter your last name")]

    public string LastName { get; set; } = null!;
    [Required]

    [Display(Name = "E-mail address", Prompt = "Enter your email address")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    [Display(Name = "Phone(optional)", Prompt = "Enter your email phone(optional)")]
    public string? PhoneNumber { get; set; }
    [Display(Name = "Bio(optional)", Prompt = "Add a short bio")]
    public string ?Bio { get; set; } 
}

public class AccountAddressInfo
{

    [Required]

    [Display(Name = "Address line 1", Prompt = "Enter your first address line")]

    public string AddressLine_1 { get; set; } = null!;
    [Display(Name = "Address line 2(optional)", Prompt = "Enter your second address ")]
    public string ?AddressLine_2 { get; set; }

    [Required]

    [Display(Name = "City", Prompt = "Enter your city")]
    public string City { get; set; } = null!;

    [Required]

    [Display(Name = "Postal code", Prompt = "Enter your postal code")]
    public string   PostalCode { get; set; }    = null!;
}

