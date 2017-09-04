using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Sharing: Post
    {
        public virtual Post SharedPost { get; set; }
    }
}
