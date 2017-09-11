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
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace CwirCwir.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IPostService _postService;
        private IUserService _userService;
        private ILikeService _likeService;
        private ICwirCwirDbContextService _ccDbContextService;

        public HomeController(IPostService postService, IUserService userService,ILikeService likeService,ICwirCwirDbContextService ccDbContextService)
        {
            _postService = postService;
            _userService = userService;
            _likeService = likeService;
            _ccDbContextService = ccDbContextService;
            
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

                newPost = _postService.AddWithCommit(newPost);

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
        [HttpPost]
        public IActionResult Like(int id)
        {

            var post = _postService.GetPost(id);

            var user = _userService.GetUser(User.Identity.Name);

            if(_postService.CheckIfUserLikedPost(post, user))
            {
                return RedirectToAction("Post", "Home", new { id = id });
            }

            var newLike = new Like();

            newLike.User = user;

            newLike.Post = post;

            newLike = _likeService.AddWithCommit(newLike);

            _postService.AddLike(newLike.Post, newLike);

            _ccDbContextService.Commit();


            return RedirectToAction("Post","Home",new { id=id});
        }
        [HttpPost]
        public IActionResult Response(int id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Share(int id)
        {
            return View();
        }

    } 

}


