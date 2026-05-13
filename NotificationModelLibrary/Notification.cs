using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationModelLibrary{
    public enum NotificationType{
        Email,
        SMS
    }

    public class Notification{
        public int Id { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime SentDate {get ; set;}
        public string Message {get;set;}
        public NotificationType NotificationType { get; set; }
        public int RecipientId { get; set; }
        public User? Recipient { get; set; }

        public Notification(string message, NotificationType notificationType, int recipientId){
            SentDate = DateTime.Now;
            Message = message;
            NotificationType = notificationType;
            RecipientId = recipientId;
        }

        public Notification(int id, string message, NotificationType notificationType, int recipientId, DateTime sentDate){
            Id = id;
            Message = message;
            NotificationType = notificationType;
            RecipientId = recipientId;
            SentDate = sentDate;
        }

        public override string ToString(){
            if (Recipient != null){
                string target = NotificationType == NotificationType.Email ? Recipient.Email : Recipient.PhoneNumber;
                return $"[{SentDate:yyyy-MM-dd HH:mm:ss}] {NotificationType} -> {Recipient.Name} ({target})\n  Message: {Message}";
            }
            return $"[{SentDate:yyyy-MM-dd HH:mm:ss}] {NotificationType} | RecipientId: {RecipientId} | Message: {Message}";
        }
    }
}
