using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
