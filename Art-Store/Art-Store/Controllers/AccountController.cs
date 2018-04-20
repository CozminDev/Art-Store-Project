using ArtStore.Data.Entities;
using ArtStore.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtStore.Controllers
{
    public class AccountController: Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<StoreUser> _signInManager;

        public AccountController(ILogger<AccountController> logger,SignInManager<StoreUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("returnUrl"))
                    {
                        return Redirect(Request.Query["returnUrl"]);
                    }
                    else return RedirectToAction("Shop", "App");
                }
            }
            ModelState.AddModelError("", "Failed to Login");
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }
    }
}
