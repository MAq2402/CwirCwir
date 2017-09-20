using CwirCwir.DbContexts;
using CwirCwir.Entities;
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


    }
    public class MessageService : IMessageService
    {
        private CwirCwirDbContext _context;

        public MessageService(CwirCwirDbContext context)
        {
            _context = context;
        }
        public List<Message> Messages => throw new NotImplementedException();

        public Message AddMessageWithCommit(Message newMessage)
        {
            _context.Add(newMessage);
            _context.SaveChanges();

            return newMessage;
        }
    }
}
