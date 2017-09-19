using CwirCwir.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Home
{
    public class FindUserViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}
