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
                return RedirectToAction("LackOfResults", "Message");

            }

            var userReceiver = _userService.GetUser(Receiver);

            if(userReceiver == null)
            {
                return RedirectToAction("LackOfResults", "Message");

            }

            var userSender = _userService.GetUser(Sender);          

            if(userSender.Id==userReceiver.Id)
            {
                return RedirectToAction("LackOfResults", "Message");
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

            return RedirectToAction("Conversation", "Message", new { userName = Sender, id=newMessage.Id });

            
        }
        public IActionResult LackOfResults()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Conversation(string userName,int id)
        {
            if(userName != User.Identity.Name)
            {
                return RedirectToAction("Wall", "Home");
            }

            var user = _userService.GetUser(userName);

            var message = _messageService.GetMessage(id);

            var model = new ConversationViewModel();

            var AllMessages = _messageService.Messages;


            if(message.UserReceiver.Id==user.Id)
            {
                model.Conversationalist = message.UserSender;
            }
            else if(message.UserSender.Id == user.Id)
            {
                model.Conversationalist = message.UserReceiver;

            }
            else
            {
                throw new Exception("User z tą wiadomością nie ma nic wspólnego");
            }

            model.Messages = AllMessages.Where(
                    m => m.UserReceiver.Id == message.UserReceiver.Id && m.UserSender.Id == message.UserSenderId
                    || m.UserSender.Id == message.UserReceiver.Id && m.UserReceiver.Id == message.UserSender.Id )
                    .ToList();



            return View(model);
        }

        [HttpPost]
        public IActionResult Conversation(string Sender, string Receiver,ConversationViewModel conversationViewModel)
        {
            var model = new WriteViewModel()
            {
                Sender = _userService.GetUser(Sender),
                Receiver = _userService.GetUser(Receiver),
                Content = conversationViewModel.Content
            };

            return Send(model, Sender, Receiver);
        }


    }
}
