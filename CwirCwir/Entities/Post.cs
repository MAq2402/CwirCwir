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
        public List<Response> Responses { get; set; }
        public User Author { get; set; }
        public DateTime PostDate { get; set; }
        public List<Sharing> Sharings { get; set; }
    }
}
