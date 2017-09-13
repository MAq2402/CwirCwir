using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CwirCwir.Services;

namespace CwirCwir.Controllers
{
    public class ProfileController : Controller
    {
        private IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(string id)
        {
            var user = _userService.GetUser(id);

            return View(user);
        }
    }
}
