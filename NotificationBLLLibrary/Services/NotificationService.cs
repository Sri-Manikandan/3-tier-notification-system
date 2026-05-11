using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NotificationBLLLibrary.Interfaces;
using NotificationDALLibrary.Interfaces;
using NotificationDALLibrary.Repositories;
using NotificationModelLibrary;
using NotificationModelLibrary.Exceptions;

namespace NotificationBLLLibrary.Services{
    public class NotificationService {
        private INotificationRepository<Notification> _repository;
        private IUserService _userService;
        private Dictionary<NotificationType, INotificationSender> _senders;

        public NotificationService() {
            _repository = new NotificationRepository();
            _userService = new UserService();
            _senders = new Dictionary<NotificationType, INotificationSender> {
                { NotificationType.Email, new EmailNotificationSender() },
                { NotificationType.SMS,   new SMSNotificationSender() }
            };
        }

        public void SendNotification(string message, NotificationType type, User user){
            ValidateUser(user, type);
            ValidateMessage(message, type);

            if (!_senders.TryGetValue(type, out INotificationSender? sender) || sender == null) {
                throw new CustomException($"No sender configured for notification type '{type}'.");
            }

            _userService.GetOrAddUser(user, type);

            Notification notification = new Notification(message, type, user);
            sender.Send(user, notification);
            _repository.Add(notification);
        }

        public List<Notification> GetAllNotifications() {
            return _repository.GetAll();
        }

        private void ValidateUser(User user, NotificationType type) {
            if (user == null) {
                throw new CustomException("User details are required.");
            }
            if (string.IsNullOrWhiteSpace(user.Name)) {
                throw new CustomException("User name is required.");
            }
            if (type == NotificationType.Email) {
                if (string.IsNullOrWhiteSpace(user.Email) ||
                    !Regex.IsMatch(user.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")) {
                    throw new CustomException("A valid email address is required for Email notifications.");
                }
            }else if (type == NotificationType.SMS) {
                if (string.IsNullOrWhiteSpace(user.PhoneNumber) ||
                    !Regex.IsMatch(user.PhoneNumber, @"^\d{10}$")) {
                    throw new CustomException("A valid 10-digit phone number is required for SMS notifications.");
                }
            }
        }

        private void ValidateMessage(string message, NotificationType type) {
            if (string.IsNullOrWhiteSpace(message)) {
                throw new CustomException("Message cannot be empty.");
            }
            if (message.Trim().Length < 5) {
                throw new CustomException("Message should be at least 5 characters long.");
            }
            if (type == NotificationType.SMS && message.Length > 160) {
                throw new CustomException("SMS message should not exceed 160 characters.");
            }
        }
    }
}
