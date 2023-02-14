using DataAccessLayer.Interfaces;
using messenger2.DataLayer.ViewModels.Account;
using messenger2.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace messenger2.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel registrationModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Register(registrationModel);
                if (response.StatusCode == DataLayer.Enums.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                    return RedirectToAction("Index", controllerName: "Home");
                }
                ModelState.AddModelError(key: "", errorMessage: response.Description);
            }
            return View(registrationModel);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(loginModel);
                if (response.StatusCode == DataLayer.Enums.StatusCode.OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                    return RedirectToAction("Index", controllerName: "Home");
                }
                ModelState.AddModelError(key: "", errorMessage: response.Description);
            }
            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", controllerName: "Home");
        }
    }
}
