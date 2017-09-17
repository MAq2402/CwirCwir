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
            Likes = new List<ResponseLike>();
            ResponseDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual List<ResponseLike> Likes { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime ResponseDate { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
