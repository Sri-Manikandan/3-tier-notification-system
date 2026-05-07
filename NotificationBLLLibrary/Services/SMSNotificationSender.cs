using System;
using NotificationBLLLibrary.Interfaces;
using NotificationModelLibrary;

namespace NotificationBLLLibrary.Services{

    public class SMSNotificationSender : INotificationSender{
        public void Send(User user, Notification notification){
            Console.WriteLine($"SMS sent to {user.PhoneNumber}: {notification.Message}");
        }
    }
}
