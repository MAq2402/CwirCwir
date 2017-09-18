using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CwirCwir.Services;
using Microsoft.AspNetCore.Authorization;

namespace CwirCwir.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(string name)
        {
            var user = _userService.GetUser(name);

            return View(user);
        }
    }
}
