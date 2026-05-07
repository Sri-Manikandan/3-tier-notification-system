using System.Collections.Generic;
using NotificationModelLibrary;
using NotificationDALLibrary.Interfaces;

namespace NotificationDALLibrary.Repositories{
    public class NotificationRepository : INotificationRepository<Notification>
    {
        private List<Notification> _notifications;

        public NotificationRepository()
        {
            _notifications = new List<Notification>();
        }

        public void Add(Notification notification)
        {
            _notifications.Add(notification);
        }

        public List<Notification> GetAll()
        {
            return _notifications;
        }
    }
}
