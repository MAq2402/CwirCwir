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
        public User()
        {
            Posts = new List<Post>();
            Messages = new List<Message>();
        }
        
        public virtual List<Post> Posts { get; set; }
        public virtual List<Message> Messages { get; set; }


        /*public virtual List<User> FollowedUsers { get; set; }
        public virtual List<User> UserFollowers { get; set; }*/

    }
}
