using System;
using NotificationModelLibrary;

namespace NotificationBLLLibrary.Interfaces{

    public interface INotificationSender{
        void Send(User user, Notification notification);

    }
}
