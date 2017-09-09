using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Like
    {
        public int Id { get; set; }

        public List<User> Users { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
    }
}
