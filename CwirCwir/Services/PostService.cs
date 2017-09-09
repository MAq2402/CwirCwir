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
        Post Add(Post newPost);
        Post GetPost(int id);
        void Commit();
        List<Post> Posts { get; }
        void AddLike(int PostId);

    }

    public class DateComparer : IComparer<Post>
    {
        public int Compare(Post x, Post y)
        {
            return - x.PostDate.CompareTo(y.PostDate);
        }
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
                List<Post> posts = _context.Posts.Include(p => p.User).ToList();
                posts.Sort(new DateComparer());
                return posts;
            }
        }

        public Post Add(Post newPost)
        {
            _context.Add(newPost);
            _context.SaveChanges();
            return newPost;
        }

        public void AddLike(int PostId)
        {
            
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public Post GetPost(int id)
        {
            return Posts.FirstOrDefault(p => p.Id == id);
        }
    }
}
