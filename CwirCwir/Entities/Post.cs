﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public class Post
    {
        public Post()
        {
            Responses = new List<Response>();
            Sharings = new List<Sharing>();
            Likes = new List<Like>();
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public List<Like> Likes { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public  DateTime PostDate { get; set; }
        public virtual List<Response> Responses { get; set; }
        public virtual List<Sharing> Sharings { get; set; }
    }
}
