using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CwirCwir.Entities
{
    public class User:IdentityUser
    {
        public User()
        {
            Posts = new List<Post>();
            ReceivedMessages = new List<Message>();
            SentMessages = new List<Message>();
            Notifications = new List<Notification>();
        }
        
        public virtual List<Post> Posts { get; set; }
        [InverseProperty("UserReceiver")]
        public virtual List<Message> ReceivedMessages { get; set; }
        [InverseProperty("UserSender")]
        public virtual List<Message> SentMessages { get; set; }
        [InverseProperty("NotifiedUser")]
        public virtual List<Notification> Notifications { get; set; }
    }
}
