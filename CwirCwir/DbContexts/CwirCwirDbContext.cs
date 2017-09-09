using CwirCwir.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.DbContexts
{
    public class CwirCwirDbContext : IdentityDbContext<User>
    {
        public CwirCwirDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Sharing> Sharings { get; set; }
        public DbSet<Like> Likes { get; set; }
    }
}
