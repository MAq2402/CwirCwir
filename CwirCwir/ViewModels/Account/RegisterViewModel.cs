using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Podaj Nazwę Użytkownika"),Display(Name ="Nazwa Użytkownika")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage ="Wprowadź hasło"),Display(Name ="Hasło"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Wprowadź hasło ponownie"),Display(Name ="Potwierdź Hasło"),Compare(nameof(Password)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
