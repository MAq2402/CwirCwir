﻿using System;
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
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public DateTime MessageDate { get; set; }

    }
}
