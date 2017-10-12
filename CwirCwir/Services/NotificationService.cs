using CwirCwir.DbContexts;
using CwirCwir.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Services
{
    public interface INotificationService
    {
        void AddNotification(Notification newNotification);
        IEnumerable<Notification> Notifications { get; }
    }
    public class NotificationService : INotificationService
    {
        private CwirCwirDbContext _context;

        public NotificationService(CwirCwirDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Notification> Notifications => _context.Notifications.Include(n => n.NotifiedUser)
                                                                                .Include(n => n.NotifyingUser)
                                                                                .Include(n=>n.Post)
                                                                                .OrderByDescending(n => n.Date)
                                                                                .ToList();

        public void AddNotification(Notification newNotification)
        {
            _context.Notifications.Add(newNotification);
        }

    }
}
