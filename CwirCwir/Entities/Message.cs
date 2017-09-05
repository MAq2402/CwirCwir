using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }

    }
}
