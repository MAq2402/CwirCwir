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
        public virtual List<Response> Responses { get; set; }
        public virtual User Author { get; set; }
        public virtual DateTime PostDate { get; set; }
        public virtual List<Sharing> Sharings { get; set; }
    }
}
