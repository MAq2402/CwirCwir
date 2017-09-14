using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Response
    {
        public Response()
        {
            Likes = new List<Like>();
            PostDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public List<Like> Likes { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime PostDate { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
