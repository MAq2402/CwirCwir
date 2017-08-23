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

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Wall", "Home");
            }
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {


                var LoginResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password,true, false);

                if(LoginResult.Succeeded)
                {
                    return RedirectToAction("Wall", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wystąpił błąd, spróbuj zalogować się ponownie");
                    return View();// dodac model? 
                }
            }
            return View();       
        }
        [HttpGet]
        public IActionResult Register()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Wall", "Home");
            }
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
                    await _signInManager.SignInAsync(user,false);

                    return RedirectToAction("Wall", "Home");
                }
                else
                {
                    PrintErrors(createResult);
                }
            }
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index","Home");
        }

        private void PrintErrors(IdentityResult createResult)
        {
            foreach (var error in createResult.Errors)
            {

                if (error.Code == "PasswordTooShort")
                {
                    ModelState.AddModelError("", "Hasło musi posiadać conajmniej 6 znaków");
                }
                else if (error.Code == "PasswordRequiresNonAlphanumeric")
                {
                    ModelState.AddModelError("", "Hasło musi posiadać przynajmniej jeden niealfanumeryczny znak");
                }
                else if (error.Code == "PasswordRequiresDigit")
                {
                    ModelState.AddModelError("", "Hasło musi posiadać przynajmniej jedną cyfrę");
                }
                else if (error.Code == "PasswordRequiresUpper")
                {
                    ModelState.AddModelError("", "Hasło musi posiadać przynajmniej jedną wielką literę");
                }
                else
                {
                    ModelState.AddModelError("", "Twoje Dane do rejestracji są błędne");
                }
            }
        }
    }
}
