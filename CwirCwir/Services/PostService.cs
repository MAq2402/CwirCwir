using CwirCwir.DbContexts;
using CwirCwir.Entities;
using CwirCwir.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CwirCwir.Services
{
    public interface IPostService
    {
        void Add(Post newPost);

        void Commit();
        IEnumerable<Post> GetAll();

    }
    public class PostServiceInMemory : IPostService
    {
        public PostServiceInMemory(IUserService userService)
        {
            _userService = userService;

            postData = new List<Post>()
            {
                new Post {Id=1,Content="Uczymy się", Likes = 0,
                            Author = _userService.GetUser("MAq") },
                new Post {Id=1,Content="Jest Cudownie",Likes=0, Author =_userService.GetUser("MAq")},

                new Post {Id=1,Content="Jest Progres",Likes=0, Author =_userService.GetUser("MAq")}
            };
        }

       
        public void Add(Post newPost)
        {
            postData.Add(newPost);
        }
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            return postData;
        }


        private readonly List<Post> postData;
        private CwirCwirDbContext _context;
        private IUserService _userService;
    }
}
