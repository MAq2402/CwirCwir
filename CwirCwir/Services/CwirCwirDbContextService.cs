using CwirCwir.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Services
{
    public interface ICwirCwirDbContextService
    {
        void Commit();
    }
    public class CwirCwirDbContextService:ICwirCwirDbContextService
    {
        private CwirCwirDbContext _context;

        public CwirCwirDbContextService(CwirCwirDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {

            _context.SaveChanges();
        }
    }
}
