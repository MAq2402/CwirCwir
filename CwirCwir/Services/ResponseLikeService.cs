using CwirCwir.DbContexts;
using CwirCwir.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Services
{
    public interface IResponseLikeService
    {
        ResponseLike AddWithCommit(ResponseLike newLike);
        List<ResponseLike> Likes { get; }
    }
    public class ResponseLikeService:IResponseLikeService
    {
        private CwirCwirDbContext _context;

        public ResponseLikeService(CwirCwirDbContext context)
        {
            _context = context;
        }

        public List<ResponseLike> Likes => _context.ResponseLikes.Include(l => l.Response).Include(l => l.User).ToList();

        public ResponseLike AddWithCommit(ResponseLike newLike)
        {
            Likes.Add(newLike);
            _context.SaveChanges();

            return newLike;
        }
    }
}
