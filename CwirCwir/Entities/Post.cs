using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public IEnumerable<Response> Responses { get; set; }
        public User Author { get; set; }
    }
}
