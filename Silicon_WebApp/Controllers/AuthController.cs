
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Silicon_WebApp.ViewModels;

namespace Silicon_WebApp.Controllers
{
    public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, ApplicationContext context) : Controller
    {
        private readonly UserManager<UserEntity> _userManager = userManager;
        private readonly SignInManager<UserEntity> _signInManager = signInManager;
        private readonly ApplicationContext _context = context;

        [HttpGet]
        [Route("/signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [Route("/signup")]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _context.Users.AnyAsync(x => x.Email == model.Email))
                {
                    var userEntity = new UserEntity
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        UserName = model.Email
                    };

                    var result = await _userManager.CreateAsync(userEntity, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(userEntity, "DefaultRole"); // Add user to a role if necessary
                        var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                        if (signInResult.Succeeded)
                        {
                            return LocalRedirect("/");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Unable to sign in.");
                            return View(model);
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "User with the same email already exists");
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        [Route("/signin")]
        public IActionResult SignIn(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("/signin")]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.IsPersistent, false);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect email or password.");
                    return View(model);
                }
            }

            return View(model);
        }

        [Route("/signout")]
        public  new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Home", "Default");
        }
    }
}
