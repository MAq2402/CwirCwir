using CwirCwir.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Message
{
    public class ConversationViewModel
    {
        public User User { get; set; }
        public User Conversationalist { get; set; }
        public List<CwirCwir.Entities.Message> Messages { get; set; }
        public string Content { get; set; }
    }
}
