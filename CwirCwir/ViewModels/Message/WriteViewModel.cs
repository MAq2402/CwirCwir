using CwirCwir.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Message
{
    public class WriteViewModel
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string  Content { get; set; }
    }
}
