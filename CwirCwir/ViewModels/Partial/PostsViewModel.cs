using CwirCwir.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Partial
{
    public class PostsViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
