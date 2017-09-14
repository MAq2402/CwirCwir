using CwirCwir.Comparers;
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
                                                   .ToList();
                responses.Sort(new ResponseDateComparer());
                return responses;
                
            }
        }

        public Response AddResponseWithCommit(Response newResponse)
        {
            _context.Add(newResponse);
            _context.SaveChanges();
            return newResponse;
        }
    }
}
