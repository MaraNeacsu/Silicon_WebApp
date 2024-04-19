


using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Silicon_WebApp.ViewModels;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Security.Claims;


namespace Silicon_WebApp.Controllers;

[Authorize]
public class AccountController(UserManager<UserEntity> userManager, ApplicationContext context) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ApplicationContext _context = context;

    public async Task<IActionResult> Details()
    {
        var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == nameIdentifier);

        var viewModel = new AccountDetailsViewModel
        {
            Basic = new AccountBasicInfo
            {
                FirstName = user!.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                Bio = user.Bio
            },
            Address= new AccountAddressInfo
            {
                AddressLine_1 = user.Address?.AddressLine_1 ?? string.Empty,
                AddressLine_2 = user.Address?.AddressLine_2,
                City = user.Address?.City ?? string.Empty,
                PostalCode = user.Address?.PostalCode ?? string.Empty
            }
            
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> UpdateBasicInfo(AccountDetailsViewModel model)
    {
        if (TryValidateModel(model.Basic!))
        {

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.FirstName = model.Basic!.FirstName;
                user.LastName = model.Basic!.LastName;
                user.Email = model.Basic!.Email;
                user.PhoneNumber = model.Basic!.PhoneNumber;
                user.UserName = model.Basic!.Email;
                user.Bio = model.Basic!.Bio;


                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    TempData["StatusMessage"] = " Updated  basic information successfully";
                }
                else
                {
                    TempData["StatusMessage"] = "Unable to save basic information";
                }
            }
        }
        else
        {
            TempData["StatusMessage"] = "Unable to save basic information";
        }

        return RedirectToAction("Details", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAddressInfo(AccountDetailsViewModel model)
    {
        if (model.Address == null)
        {
            TempData["StatusMessage"] = "Address data is null";
            return RedirectToAction("Details", "Account");
        }

        if (!TryValidateModel(model.Address))
        {
            TempData["StatusMessage"] = "Validation failed for address data";
            return RedirectToAction("Details", "Account");
        }

        var nameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(nameIdentifier))
        {
            TempData["StatusMessage"] = "User identifier not found";
            return RedirectToAction("Details", "Account");
        }

        var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == nameIdentifier);
        if (user == null)
        {
            TempData["StatusMessage"] = "User not found";
            return RedirectToAction("Details", "Account");
        }

        try
        {
            if (user.Address == null)
            {
                user.Address = new AddressEntity();
            }

            user.Address.AddressLine_1 = model.Address!.AddressLine_1;
            user.Address.AddressLine_2 = model.Address!.AddressLine_2;
            user.Address.PostalCode = model.Address.PostalCode;
            user.Address.City = model.Address.City;

            _context.Update(user);
            await _context.SaveChangesAsync();
            TempData["StatusMessage"] = "Updated address information successfully";
        }
        catch
        {
            TempData["StatusMessage"] = $"Unable to save address information";
        }

        return RedirectToAction("Details", "Account");
    }



    [HttpPost]
    public  async Task<IActionResult>UploadProfileImage(IFormFile file)
    {
        var user = await _userManager.GetUserAsync(User);
        if (file != null && file!= null && file.Length != 0)
        {

            var fileName = $"p_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads/profiles", fileName);
     
            using var fs= new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fs);
            user.ProfileImage = fileName;
            await _userManager.UpdateAsync(user);

                
            
        }
        else 
        {

            TempData["StatusMessage"] = "Unable upload profile image";

        }

        return RedirectToAction("Details", "Account");

    }
       
}
