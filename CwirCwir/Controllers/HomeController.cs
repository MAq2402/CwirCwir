using CwirCwir.Entities;
using CwirCwir.Services;
using CwirCwir.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IPostService _postService;
        private IUserService _userService;

        public HomeController(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Wall", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Wall()
        {
            return View();
        }
    }

        //[HttpPost, ValidateAntiForgeryToken]
        /*public IActionResult Wall(WallViewModel model)
        {
            var newPost = new Post();

            newPost.Content = model.Content;

            newPost.Likes = 0;

            var userName = User.Identity.Name;

            newPost.Author = _userService.GetUser(userName);

            _postService.Add(newPost);

            return View(model);
            
            
        }*/
    
}
