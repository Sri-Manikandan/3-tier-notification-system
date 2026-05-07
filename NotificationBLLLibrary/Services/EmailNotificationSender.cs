using System;
using NotificationBLLLibrary.Interfaces;
using NotificationModelLibrary;

namespace NotificationBLLLibrary.Services{

    public class EmailNotificationSender : INotificationSender{
        public void Send(User user, Notification notification){
            Console.WriteLine($"Email sent to {user.Email}: {notification.Message}");
        }
    }
}
