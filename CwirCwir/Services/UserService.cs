using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CwirCwir.Entities;
using CwirCwir.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CwirCwir.Services
{
    public interface IUserService
    {
        User GetUser(string UserName);

        void AddPost(Post newPost);
    }
    public class UserService : IUserService
    {
        private CwirCwirDbContext _context;

        public UserService(CwirCwirDbContext context)
        {
            _context = context;
        }

        public void AddPost(Post newPost)
        {
            
        }

        public User GetUser(string UserName)
        {
           return _context.Users.FirstOrDefault(x => x.UserName == UserName);
           
        }
        

    }
}
