
using CwirCwir.DbContexts;
using CwirCwir.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Services
{
    public interface IResponseService
    {
        Response AddResponseWithCommit(Response newResponse);
        List<Response> responses { get; }
        void AddLike(Response response, ResponseLike newLike);
        Response GetResponse(int id);
        bool CheckIfUserLikedResponse(Response response,User user);

    }


    public class ResponseService : IResponseService
    {
        private CwirCwirDbContext _context;

        public ResponseService(CwirCwirDbContext context)
        {
            _context = context;
        }

        public List<Response> responses
        {
            get
            {
                List<Response> responses = _context.Responses.Include(r => r.Likes)
                                                   .Include(r => r.Post)
                                                   .Include(r => r.User)
                                                   .OrderByDescending(r => r.ResponseDate)
                                                   .ToList();
                
                return responses;
                
            }
        }

        public void AddLike(Response response, ResponseLike newLike)
        {
            responses.FirstOrDefault(r => r.Id == response.Id)
                     .Likes
                     .Add(newLike);
        }

        public Response AddResponseWithCommit(Response newResponse)
        {
            _context.Add(newResponse);
            _context.SaveChanges();
            return newResponse;
        }

        public bool CheckIfUserLikedResponse(Response response,User user)
        {
            if(response.Likes.Any(l=>l.User==user))
            {
                return true;
            }
            return false;
        }

        public Response GetResponse(int id)
        {
            return responses.FirstOrDefault(r => r.Id == id);
        }
    }
}
