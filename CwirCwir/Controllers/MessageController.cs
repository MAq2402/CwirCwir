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

            var user = _userService.GetUser(name);

            var model = new IndexViewModel
            {
                user = user,
                Messages = _messageService.Messages
                                          .Where(m=>m.UserReceiver.Id==user.Id || m.UserSender.Id==user.Id)
                                          .ToList()
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
        public IActionResult Send(WriteViewModel writeViewModel,string Sender,string Receiver)
        {
            if(Sender!=User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            if (String.IsNullOrEmpty(Receiver))
                throw new Exception("No receiver");

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

        public IActionResult Conversation(string userName,int id)
        {
            if(userName != User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            var user = _userService.GetUser(userName);

            var message = _messageService.GetMessage(id);

            var model = new ConverstationViewModel();

            //model.Sender = user;

                

                ////messages = user.sentmessages
                ////                            .where(m => m.userreceiver.id == message.userreceiver.id)
                ////                            .concat(
                ////                                   user.receivedmessages.where(m => m.usersender.id == message.usersender.id)
                ////                                   ).tolist()

            if(message.UserReceiver.Id==user.Id)
            {
                //tutaj troche inaczej wiesz o co cho zią:)
            }
            else if(message.UserSender.Id == user.Id)
            {
                model.Messages = user.SentMessages
                                           .Where(m => m.UserReceiver.Id == message.UserReceiver.Id)
                                            .Concat(
                                                   user.ReceivedMessages.Where(m => m.UserSender.Id == message.UserSender.Id)
                                                   ).ToList();
            }
            else
            {
                throw new Exception("User z tą wiadomością nie ma nic wspólnego");
            }


            return View(model);
        }


    }
}
