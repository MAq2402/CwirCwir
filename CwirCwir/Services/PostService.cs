
using CwirCwir.DbContexts;
using CwirCwir.Entities;
using CwirCwir.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CwirCwir.Services
{
    public interface IPostService
    {
        Post AddWithCommit(Post newPost);
        Post GetPost(int id);
        List<Post> Posts { get; }
        void AddLike(Post post,Like newLike);
        void AddResponse(Post post, Response newResponse);
        void AddLikeToResponse(Post post, Response response, ResponseLike responseLike);
        bool CheckIfUserLikedPost(Post post,User user);

        IEnumerable<Response> GetResponses(Post post);

    }


    public class PostService : IPostService
    {
        private CwirCwirDbContext _context;
        private IResponseService _responseService;

        public PostService(CwirCwirDbContext context, IResponseService responseService)
        {
            _context = context;
            _responseService = responseService;
        }

        public List<Post> Posts
        {
            get
            {
                List<Post> posts = _context.Posts.Include(p => p.User)
                                                 .Include(p => p.Likes)
                                                 .Include(p => p.Responses)
                                                 .OrderByDescending(p => p.PostDate)
                                                 .ToList();

                return posts;
            }
        }

        public Post AddWithCommit(Post newPost)
        {
            _context.Add(newPost);
            _context.SaveChanges();
            return newPost;
        }

        public void AddLike(Post post,Like newLike)
        {
            Posts.FirstOrDefault(x=>x.Id==post.Id)
                 .Likes
                 .Add(newLike);
        }

        public void AddResponse(Post post,Response newResponse)
        {
            Posts.FirstOrDefault(p => p.Id == post.Id)
                  .Responses
                  .Add(newResponse);
        }
        public void AddLikeToResponse(Post post, Response response, ResponseLike responseLike)
        {
            Posts.FirstOrDefault(p => p.Id == post.Id)
                 .Responses.FirstOrDefault(r => r.Id == response.Id)
                 .Likes.Add(responseLike);
        }

        public Post GetPost(int id)
        {
            return Posts.FirstOrDefault(p => p.Id == id);
        }

        public bool CheckIfUserLikedPost(Post post,User user)
        {
            if(post.Likes.Any(l=>l.User==user))
            {
                return true;
            }
            return false;
        }
        public IEnumerable<Response> GetResponses(Post post)
        {
            return _responseService.responses.Where(r => r.PostId == post.Id);
        }
    }
}
