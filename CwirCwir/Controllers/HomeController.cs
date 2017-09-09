using CwirCwir.Entities;
using CwirCwir.Services;
using CwirCwir.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Wall(WallViewModel wallViewModel)
        {
            if (ModelState.IsValid)
            {
                var newPost = new Post();

                newPost.Content = wallViewModel.Content;

                
                newPost.PostDate = DateTime.Now;

                var userName = User.Identity.Name;

                newPost.User = _userService.GetUser(userName);

                newPost = _postService.Add(newPost);

                return RedirectToAction("Post", new { newPost.Id });
            }
            return View();

        }
        public IActionResult Post(int id)
        {
            var model = new PostViewModel();
            model.post = _postService.GetPost(id);

            return View(model);
        }

    } 

}


