using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Sharing
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public virtual List<Response> Responses { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime PostDate { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
