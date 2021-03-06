﻿using System;
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

        IEnumerable<User> Users { get;  }
        void AddPost(User user, Post post);
        void AddReceivedMessage(Message message, User user);
        void AddSentMessage(Message message, User user);

    }

    public class UserService : IUserService
    {
        private CwirCwirDbContext _context;

        public UserService(CwirCwirDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> Users { get => _context.Users.Include(u => u.Posts)
                                                               .Include(u => u.SentMessages)
                                                                .Include(u => u.ReceivedMessages);
        }

        public void AddPost(User user, Post post)
        {
            Users.FirstOrDefault(u => u.Id == user.Id).Posts.Add(post);
        }

        public void AddReceivedMessage(Message message, User user)
        {
            Users.FirstOrDefault(u => u.Id == user.Id).ReceivedMessages.Add(message);
        }

        public void AddSentMessage(Message message, User user)
        {
            Users.FirstOrDefault(u => u.Id == user.Id).SentMessages.Add(message);
        }

        public User GetUser(string UserName)
        {
           return _context.Users.FirstOrDefault(x => x.UserName == UserName);
            
           
        }
        

    }
}
