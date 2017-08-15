using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
    }
}
