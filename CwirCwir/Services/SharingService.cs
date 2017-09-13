using CwirCwir.DbContexts;
using CwirCwir.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Services
{
    public interface ISharingService
    {
        IEnumerable<Sharing> Sharings { get; }
    }
    public class SharingService : ISharingService
    {
        private CwirCwirDbContext _context;

        public SharingService(CwirCwirDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Sharing> Sharings => _context.Sharings.Include(s => s.Post)
                                                                 .Include(s => s.Responses)
                                                                 .Include(s => s.Likes)
                                                                 .Include(s => s.User);
    }
}
