using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Message
    {
        public Message()
        {
            MessageDate = DateTime.Now;
            IsRead = false;
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual User UserSender { get; set; }
        public string UserSenderId { get; set; }
        public virtual User UserReceiver { get; set; }
        public string UserReceiverId { get; set; }
        public DateTime MessageDate { get; set; }
        public bool IsRead { get; set; }

    }
}
