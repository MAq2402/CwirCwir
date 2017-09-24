using CwirCwir.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Home
{
    public class PostViewModel
    {
        public Post post { get; set; }
        public IEnumerable<Response> Responses { get; set; }

        [Required(ErrorMessage = "Twoja odpowiedź nie zawiera żadnej treści."),MaxLength(150,ErrorMessage ="Maksymalna długość wynosi 150 znaków.")]
        public string Content { get; set; }
    }
}
