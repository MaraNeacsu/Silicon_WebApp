using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities;

public class UserEntity:IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? ProfileImage { get; set; } = "avatar.png";


   public string? Bio { get; set; }

    public int? AdressId { get; set; }
    public AdressEntity? Adress { get; set; }



}




