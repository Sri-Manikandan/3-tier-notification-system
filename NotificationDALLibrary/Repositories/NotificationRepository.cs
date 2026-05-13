using Microsoft.EntityFrameworkCore;
using NotificationDALLibrary.Interfaces;
using NotificationModelLibrary;

namespace NotificationDALLibrary.Repositories
{
    public class NotificationRepository : AbstractRepository<Notification>
    {
        public override List<Notification> GetAll()
        {
            return context.Notifications.Include(n => n.Recipient).ToList();
        }
    }
}
