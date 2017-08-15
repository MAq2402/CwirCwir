using CwirCwir.Entities;
using CwirCwir.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Home
{
    public class IndexViewModel
    {
        [Display(Name = "Ćwirnij coś"), MaxLength(150), MinLength(1)]
        public string Content { get; set; }

    }
}
