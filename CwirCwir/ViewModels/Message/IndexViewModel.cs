using CwirCwir.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Message
{
    public class IndexViewModel
    {
        public User user { get; set; }
        public List<CwirCwir.Entities.Message> Messages { get; set; }
    }
}
