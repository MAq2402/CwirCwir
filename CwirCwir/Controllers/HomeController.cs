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
        private INotificationService _notificationService;

        public HomeController(IPostService postService,
                                IUserService userService,
                                ILikeService likeService,
                                ICwirCwirDbContextService ccDbContextService,
                                IResponseService responseService,
                                IResponseLikeService responseLikeService,
                                INotificationService notificationService)
        {
            _postService = postService;
            _userService = userService;
            _likeService = likeService;
            _ccDbContextService = ccDbContextService;
            _responseService = responseService;
            _responseLikeService = responseLikeService;
            _notificationService = notificationService;
            
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
            var model = new WallViewModel();

            model.Posts = _postService.Posts;

            return View(model);
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
            if(!ModelState.IsValid)
            {
                wallViewModel.Posts = _postService.Posts;

                return View(wallViewModel);
            }

            var newPost = new Post();

            newPost.Content = wallViewModel.Content;

            var user = _userService.GetUser(User.Identity.Name);

            newPost.User = user;

            newPost = _postService.AddWithCommit(newPost);

            _userService.AddPost(user, newPost);

            _ccDbContextService.Commit();

            wallViewModel.Posts = _postService.Posts;

            return View(wallViewModel);

        }
        [HttpGet]
        public IActionResult Post(int id)
        {
            var model = new PostViewModel();

            var post = _postService.GetPost(id);

            model.post = post;

            model.Responses = _postService.GetResponses(post);

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

            var newNotification = new Notification
            {
                NotifiedUser = post.User,
                NotifyingUser = user,
                NotificationType = NotificationType.Like,
                Post = post
                
            };

            _notificationService.AddNotification(newNotification);

            _ccDbContextService.Commit();


            return RedirectToAction("Post","Home",new { id=id});
        }
        [HttpPost,ValidateAntiForgeryToken]
        public IActionResult Post(PostViewModel postViewModel,int id)
        {
            

            var post = _postService.GetPost(id);

           postViewModel.post = post;        

            if (!ModelState.IsValid)
            {
                postViewModel.Responses = _postService.GetResponses(post);

                return View(postViewModel);
            }

            var newResponse = new Response();

            var user = _userService.GetUser(User.Identity.Name);


            newResponse.Content = postViewModel.Content;
            newResponse.Post = post;
            newResponse.User = user;
            newResponse = _responseService.AddResponseWithCommit(newResponse);

            _postService.AddResponse(post, newResponse);

            var newNotification = new Notification
            {
                NotifiedUser = post.User,
                NotifyingUser = user,
                NotificationType = NotificationType.Response,
                Post = post

            };

            _notificationService.AddNotification(newNotification);

            _ccDbContextService.Commit();

            postViewModel.Responses = _postService.GetResponses(post); // zeby nowa odpowiedz byla zawarta

            return View(postViewModel);
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

            var newNotification = new Notification
            {
                NotifiedUser = response.User,
                NotifyingUser = user,
                NotificationType = NotificationType.LikeResponse,
                Post = post

            };

            _notificationService.AddNotification(newNotification);

            _ccDbContextService.Commit();

            return RedirectToAction("Post", "Home", new { id = PostId });
        }
     

    } 

}


