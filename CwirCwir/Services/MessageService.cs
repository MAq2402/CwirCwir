using CwirCwir.Comparers;
using CwirCwir.DbContexts;
using CwirCwir.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Services
{
    public interface IMessageService
    {
        Message AddMessageWithCommit(Message newMessage);
        List<Message> Messages { get; }

        Message GetMessage(int id);


    }
    public class MessageService : IMessageService
    {
        private CwirCwirDbContext _context;

        public MessageService(CwirCwirDbContext context)
        {
            _context = context;
        }
        public List<Message> Messages
        {
            get
            {
                var messages = new List<Message>();

                messages = _context.Messages.Include(m => m.UserReceiver)
                                            .Include(m => m.UserSender)
                                            .ToList();

                messages.Sort(new MessageDateComparer());

                return messages;
                                            
            }
        }

        public Message AddMessageWithCommit(Message newMessage)
        {
            _context.Add(newMessage);
            _context.SaveChanges();

            return newMessage;
        }

        public Message GetMessage(int id)
        {

            return Messages.FirstOrDefault(m => m.Id == id);
        }
    }
}
