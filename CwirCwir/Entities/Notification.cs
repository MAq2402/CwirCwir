using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CwirCwir.Entities
{
    public enum NotificationType
    {
        Like,
        Response,
        LikeResponse
    }
    public class Notification
    {
        public Notification()
        {
            Date = DateTime.Now;
            IsRead = false;
        }
        public int Id { get; set; }
        public string NotifiedUserId { get; set; }
        public User NotifiedUser { get; set; }
        public string NotifyingUserId { get; set; }
        public User NotifyingUser { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
