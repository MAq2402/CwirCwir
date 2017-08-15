using CwirCwir.Entities;
using CwirCwir.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Controllers
{
    public class AccountController:Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager,UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]

        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {


                var LoginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password,false, false);

                if(LoginResult.Succeeded)
                {
                    if(Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wystąpił błąd, spróbuj zalogować się ponownie");
                    return View();// dodac model? 
                }
            }
            return RedirectToAction("Index", "Home");
            
            
            
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost,AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName };

                var createResult = await _userManager.CreateAsync(user, model.Password);

                if(createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Home");

                    
                }
                else
                {
                    foreach(var error in createResult.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login","Account");
        }


    }
}
