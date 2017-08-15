﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required,Display(Name = "Nazwa Użytkownika")]
        public string UserName { get; set; }

        [Required,DataType(DataType.Password),Display(Name ="Hasło")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
