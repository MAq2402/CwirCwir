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
        private IResponseService _responseService;
        private IResponseLikeService _responseLikeService;

        public HomeController(IPostService postService,
                                IUserService userService,
                                ILikeService likeService,
                                ICwirCwirDbContextService ccDbContextService,
                                IResponseService responseService,
                                IResponseLikeService responseLikeService)
        {
            _postService = postService;
            _userService = userService;
            _likeService = likeService;
            _ccDbContextService = ccDbContextService;
            _responseService = responseService;
            _responseLikeService = responseLikeService;
            
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
        public IActionResult FindUser(string searchString)
        {
            var users = _userService.Users;

            if(!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                users = users.Where(u => u.UserName.ToLower().Contains(searchString));

                var model = new FindUserViewModel();

                model.Users = users;

                return View(model);
            }

            return RedirectToAction("Wall", "Home");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Wall(WallViewModel wallViewModel)
        {
            if (wallViewModel.Content!=null)
            {
                var newPost = new Post();

                newPost.Content = wallViewModel.Content;

                var user = _userService.GetUser(User.Identity.Name);

                newPost.User = user;

                newPost = _postService.AddWithCommit(newPost);

                _userService.AddPost(user, newPost);

                _ccDbContextService.Commit();
           
                return RedirectToAction("Wall","Home");
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
        public IActionResult Response(PostViewModel postViewModel,int id)
        {
            var newResponse = new Response();

            var post = _postService.GetPost(id);

            if (postViewModel.Content!=null)
            {
                

                newResponse.Content = postViewModel.Content;
                newResponse.Post = post;
                newResponse.User = _userService.GetUser(User.Identity.Name);
                newResponse = _responseService.AddResponseWithCommit(newResponse);

                _postService.AddResponse(post,newResponse);

                _ccDbContextService.Commit();
            }
            return RedirectToAction("Post","Home",new {id = post.Id });
        }

        [HttpPost]
        public IActionResult LikeResponse(int ResponseId,int PostId)
        {
            var response = _responseService.GetResponse(ResponseId);

            var user = _userService.GetUser(User.Identity.Name);

            if (_responseService.CheckIfUserLikedResponse(response, user))
            {
                return RedirectToAction("Post", "Home", new { id = PostId });
            }

            var post = _postService.GetPost(PostId);

            var newLike = new ResponseLike();
            newLike.Response = response;
            newLike.User = user;

            newLike = _responseLikeService.AddWithCommit(newLike);

            _responseService.AddLike(newLike.Response, newLike);

            _ccDbContextService.Commit();

            return RedirectToAction("Post", "Home", new { id = PostId });
        }
     

    } 

}


