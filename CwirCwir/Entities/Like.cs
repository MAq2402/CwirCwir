﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Like
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string UserId { get; set; }
        public virtual Post Post { get; set; }
        public int PostId { get; set; }
    }
}
