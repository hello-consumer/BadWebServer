using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BadWebServer.Controllers
{
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManger;

        //Using Microsoft.AspNetCore.Identity
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManger = signInManager;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Register()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> Register(Models.User model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser(model.Username);

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    var passwordResult = await _userManager.AddPasswordAsync(user, model.Password);
                    if (passwordResult.Succeeded)
                    {
                        await _signInManger.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                        passwordResult.Errors.ToList().ForEach((e) => { ModelState.AddModelError(e.Code, e.Description); });
                    }
                }
                else
                {
                    result.Errors.ToList().ForEach((e) => { ModelState.AddModelError(e.Code, e.Description); });
                }
            }
            return View();
        }

        public Microsoft.AspNetCore.Mvc.IActionResult SignIn()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> SignIn(Models.User model)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        await _signInManger.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("Username", "Email Address or Password are invalid");
            }
            return View();
        }

        public async Task<Microsoft.AspNetCore.Mvc.IActionResult> SignOut()
        {
            await _signInManger.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
