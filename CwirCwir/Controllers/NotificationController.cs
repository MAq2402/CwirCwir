using CwirCwir.Services;
using CwirCwir.ViewModels.Notification;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Controllers
{
    public class NotificationController:Controller
    {
        private INotificationService _notificationService;
        private ICwirCwirDbContextService _ccDbContextService;

        public NotificationController(INotificationService notificationService,ICwirCwirDbContextService ccDbContextService)
        {
            _notificationService = notificationService;
            _ccDbContextService = ccDbContextService;
        }
        public IActionResult Index(string name)
        {
            if(User.Identity.Name!=name)
            {
                return RedirectToAction("Wall", "Home");
            }

            var notifications = _notificationService.Notifications.Where(n => n.NotifiedUser.UserName == name).ToList();

            notifications.All(x => x.IsRead = true);

            _ccDbContextService.Commit();

            var model = new IndexViewModel()
            {
                Notifications = notifications
            };

            return View(model);
        }
    }
}
