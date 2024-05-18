using System.Security.Claims;
using CodeFood.Data;
using CodeFood.Service.Data;
using CodeFood.Service.Interfaces;
using CodeFood.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFood.Web.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(string? returnUrl = null)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View(new LoginViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginViewModel model, string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            User? user = null;

            await Task.Run(() =>
            {
                user = _userService.GetUsers()
                    .FirstOrDefault(
                        u => u.UserName == model.UserName 
                             && u.Password == Cryptography.HashPassword(model.Password));
            });

            if (user is null)
            {
                ModelState.AddModelError("", "Incorrect password or username.");
                return View(model);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Role, user.Role!)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                principal, 
                new AuthenticationProperties() { IsPersistent = true });

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }

        return View(model);
    }

    public async Task<IActionResult> LogoutAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public IActionResult Register(string? returnUrl)
    {
        @ViewBag.ReturnUrl = returnUrl!;
        return View(new RegistrationViewModel());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(RegistrationViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            //TODO Data validation

            if (_userService.GetUsers().FirstOrDefault(u => u.UserName == model.UserName) is not null)
            {
                ModelState.AddModelError(nameof(model), "Account with this credentials exist");
                return View(model);
            }

            var user = new User()
            {
                UserName = model.UserName,
                Password = Cryptography.HashPassword(model.Password),
                Email = model.Email
            };

            await Task.Run(() =>
            {
                _userService.CreateUser(user);
            });

            return Redirect(returnUrl ?? "/");
        }

        return View(model);
    }
}