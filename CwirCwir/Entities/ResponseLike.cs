using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class ResponseLike
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual Response Response { get; set; }
        public int ResponseId { get; set; }

    }
}
