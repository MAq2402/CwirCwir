using CwirCwir.Entities;
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
        private IMessageService _messageService;
        private ICwirCwirDbContextService _ccDbContextService;

        public MessageController(IUserService userService,IMessageService messageService,ICwirCwirDbContextService ccDbContextService)
        {
            _userService = userService;
            _messageService = messageService;
            _ccDbContextService = ccDbContextService;
        }
        public IActionResult Index(string name)
        {
            
            if (name!=User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            var model = new IndexViewModel
            {
                user = _userService.GetUser(name),
                Messages = _messageService.Messages
            };

            return View(model);
        }

        public IActionResult FindUser(string name)
        {
            if (name != User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            return View();

        }
        
        [HttpGet]
        public IActionResult Write(string Sender,string Receiver)
        {


            if(Sender != User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            if(String.IsNullOrEmpty(Receiver))
            {

                /*ModelState.AddModelError("", "Wpisz w pole tekstowe nazwe użytkownika, do którego chcesz napisać wiadomość.");

                return RedirectToAction("FindUser", "Message", new { name = Sender });*/

                return RedirectToAction("Wall", "Home");
                //Dodac strone brak wynikow czy cos takiego

            }

            var userReceiver = _userService.GetUser(Receiver);

            if(userReceiver == null)
            {
                return RedirectToAction("Wall", "Home");
                //Dodac strone brak wynikow czy cos takiego
            }

            var userSender = _userService.GetUser(Sender);          

            if(userSender.Id==userReceiver.Id)
            {
                /*ModelState.AddModelError("", "Nie możesz wysłać wiadomości do samego siebie.");

                return RedirectToAction("FindUser", "Message", new { name = Sender });*/

                return RedirectToAction("Wall", "Home");
                //Dodac strone brak wynikow czy cos takiego
            }

            var model = new WriteViewModel
            {
                Sender = userSender,
                Receiver = userReceiver
            };

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Write(WriteViewModel writeViewModel,string Sender,string Receiver)
        {
            if(Sender!=User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            if (String.IsNullOrEmpty(Sender))
                throw new Exception("No sender");

            if (String.IsNullOrEmpty(Receiver))
                throw new Exception("No receiver");

            if (String.IsNullOrEmpty(writeViewModel.Content))
            {
                return RedirectToAction("Message", "Write", new { Sender = Sender, Receiver = Receiver });
            }

            var newMessage = new Message();

            var userReceiver = _userService.GetUser(Receiver);
            var userSender = _userService.GetUser(Sender);

            newMessage.Content = writeViewModel.Content;
            newMessage.UserReceiver = userReceiver;
            newMessage.UserSender = userSender;

            newMessage = _messageService.AddMessageWithCommit(newMessage);

            _userService.AddReceivedMessage(newMessage, userReceiver);
            _userService.AddSentMessage(newMessage, userSender);

            _ccDbContextService.Commit();

            return RedirectToAction("Index", "Message", new { name = Sender });

            
        }


    }
}
