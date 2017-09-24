using CwirCwir.Entities;
using CwirCwir.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Home
{
    public class WallViewModel
    {
        [Display(Name = "Ćwirnij coś"), MaxLength(150,ErrorMessage ="Maksymalna dlugość wynosi 150 znaków"),Required(ErrorMessage ="Twój post jest pusty.")]
        public string Content { get; set; }
        public List<Post> Posts { get; set; }


    }
}
