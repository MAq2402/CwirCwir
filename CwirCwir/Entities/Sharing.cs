
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Sharing
    {
        public Sharing()
        {
            Responses = new List<Response>();
            Likes = new List<Like>();
            SharingDate = DateTime.Now;
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual List<Like> Likes { get; set; }
        public virtual List<Response> Responses { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime SharingDate { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}
