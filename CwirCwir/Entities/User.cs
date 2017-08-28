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
        public List<Post> Posts { get; set; }
        public List<Message> MessagesSend { get; set; }
        public List<Message> MessagesRecived { get; set; }

    }
}
