using CwirCwir.Comparers;
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
        bool CheckIfUserLikedPost(Post post,User user);

    }


    public class PostService : IPostService
    {
        private CwirCwirDbContext _context;
        public PostService(CwirCwirDbContext context)
        {
            _context = context;
        }

        public List<Post> Posts
        {
            get
            {
                List<Post> posts = _context.Posts.Include(p => p.User)
                                                 .Include(p=>p.Likes)
                                                 .Include(p=>p.Sharings)
                                                 .Include(p=>p.Responses)
                                                 .ToList();
                posts.Sort(new PostDateComparer());
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
    }
}
