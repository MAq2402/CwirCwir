using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class User:IdentityUser
    {
     
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Response> Responses { get; set; }
        public IEnumerable<Message> MessagesSend { get; set; }
        public IEnumerable<Message> MessagesRecived { get; set; }

    }
}
