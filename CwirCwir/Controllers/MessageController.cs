using CwirCwir.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Controllers
{
    [Authorize]
    public class MessageController:Controller
    {
        private IUserService _userService;

        public MessageController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(string name)
        {
            
            if (name!=User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            var user = _userService.GetUser(name);


            return View(user);
        }
        public IActionResult NewMessage(string name)
        {
            if (name != User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            var user = _userService.GetUser(name);

            return View(user);

        }
        public IActionResult FindUser(string name)
        {
            return View();
        }
        public IActionResult Write(string name)
        {
            return View();
        }
        public IActionResult Send()
        {
            return View();
        }


    }
}
