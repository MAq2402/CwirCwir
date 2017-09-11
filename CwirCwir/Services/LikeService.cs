
using CwirCwir.DbContexts;
using CwirCwir.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Services
{
    public interface ILikeService
    {
        Like AddWithCommit(Like newLike);
        List<Like> Likes { get; }
    }
    public class LikeService : ILikeService
    {
        private CwirCwirDbContext _context;

        public LikeService(CwirCwirDbContext context)
        {
            _context = context;
        }

        public List<Like> Likes => _context.Likes.Include(l => l.Post).Include(l => l.User).ToList();

        public Like AddWithCommit(Like newLike)
        {
            Likes.Add(newLike);
            _context.SaveChanges();

            return newLike;
        }

    }
}
