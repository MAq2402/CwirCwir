using CwirCwir.Services;
using CwirCwir.ViewModels.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Controllers
{
    [Authorize]
    public class MessageController:Controller
    {
        private IUserService _userService;

        public MessageController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index(string name)
        {
            
            if (name!=User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            var user = _userService.GetUser(name);


            return View(user);
        }
        public IActionResult NewMessage(string name)
        {
            if (name != User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            var user = _userService.GetUser(name);

            return View(user);

        }
        [HttpGet]
        
        public IActionResult Write(string Sender,string Receiver)
        {


            if(Sender != User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            var userSender = _userService.GetUser(Sender);

            var userReceiver = _userService.GetUser(Receiver);

            if(userSender==userReceiver)
            {
                return RedirectToAction("Wall", "Home");
            }

            var model = new WriteViewModel();

            model.Sender = userSender;
            model.Receiver = userReceiver;

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Write(WriteViewModel writeViewModel)
        {
            if(writeViewModel.Receiver==null)
                return RedirectToAction("Wall", "Home");

            

        }


    }
}
