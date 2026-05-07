using System;

namespace NotificationModelLibrary{
    public enum NotificationType{
        Email,
        SMS
    }

    public class Notification{
        public DateTime SentDate {get ; set;}
        public string Message {get;set;}
        public NotificationType NotificationType { get; set; }
        public User Recipient { get; set; }


        public Notification(string message, NotificationType notificationType, User recipient){
            SentDate = DateTime.Now;
            Message = message;
            NotificationType = notificationType;
            Recipient = recipient;
        }

        public override string ToString(){
            string target = NotificationType == NotificationType.Email ? Recipient.Email : Recipient.PhoneNumber;
            return $"[{SentDate:yyyy-MM-dd HH:mm:ss}] {NotificationType} -> {Recipient.Name} ({target})\n  Message: {Message}";
        }
    }
}
