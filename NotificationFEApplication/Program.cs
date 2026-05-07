using System;
using System.Collections.Generic;
using NotificationBLLLibrary.Services;
using NotificationModelLibrary;
using NotificationModelLibrary.Exceptions;

namespace NotificationFEApplication
{
    public class Program
    {
        private static NotificationService _service = new NotificationService();
        private static User? _currentUser;
        private static NotificationType? _type;
        private static string? _message;

        public static void Main()
        {
            Console.WriteLine("   Simple Notification System (3-Tier)");

            bool run = true;
            while (run)
            {
                ShowMenu();
                string? choice = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (choice?.Trim())
                    {
                        case "1": 
                            AddUserDetails(); 
                            break;
                        case "2": 
                            ChooseNotificationType(); 
                            break;
                        case "3": 
                            EnterNotificationMessage(); 
                            break;
                        case "4": 
                            SendNotification(); 
                            break;
                        case "5": 
                            DisplaySentNotifications(); 
                            break;
                        case "6":
                            run = false;
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select a number from 1 to 6.");
                            break;
                    }
                }
                catch (CustomException ex)
                {
                    Console.WriteLine($"[Validation] {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] Something went wrong: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("  1. Add user details");
            Console.WriteLine("  2. Choose notification type");
            Console.WriteLine("  3. Enter notification message");
            Console.WriteLine("  4. Send notification");
            Console.WriteLine("  5. Display sent notifications");
            Console.WriteLine("  6. Exit");
            Console.WriteLine("--------------------------------------------");
            Console.Write("Enter your choice: ");
        }

        private static void AddUserDetails()
        {
            Console.WriteLine("Add User Details");

            Console.Write("Enter name: ");
            string name = (Console.ReadLine() ?? string.Empty).Trim();
            Console.Write("Enter email: ");
            string email = (Console.ReadLine() ?? string.Empty).Trim();
            Console.Write("Enter phone number (10 digits): ");
            string phone = (Console.ReadLine() ?? string.Empty).Trim();

            _currentUser = new User(name, email, phone);
            Console.WriteLine("User details saved:");
            Console.WriteLine(_currentUser);
        }

        private static void ChooseNotificationType()
        {
            Console.WriteLine("Choose Notification Type");
            Console.WriteLine("1. Email");
            Console.WriteLine("2. SMS");
            Console.Write("Enter your choice: ");

            string? choice = Console.ReadLine();
            switch (choice?.Trim())
            {
                case "1":
                    _type = NotificationType.Email;
                    Console.WriteLine("Notification type set to Email.");
                    break;
                case "2":
                    _type = NotificationType.SMS;
                    Console.WriteLine("Notification type set to SMS.");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Notification type not changed.");
                    break;
            }
        }

        private static void EnterNotificationMessage()
        {
            Console.WriteLine("Enter Notification Message");
            Console.Write("Message: ");
            _message = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Message captured.");
        }

        private static void SendNotification()
        {
            Console.WriteLine("Send Notification");

            if (_currentUser == null)
            {
                throw new CustomException("Please add user details first (option 1).");
            }
            if (_type == null)
            {
                throw new CustomException("Please choose a notification type first (option 2).");
            }
            if (_message == null)
            {
                throw new CustomException("Please enter a notification message first (option 3).");
            }

            _service.SendNotification(_message, _type.Value, _currentUser);
            Console.WriteLine("Notification sent successfully.");

            _message = null;
        }

        private static void DisplaySentNotifications()
        {
            Console.WriteLine("Sent Notifications");
            List<Notification> allNotifications = _service.GetAllNotifications();

            if (allNotifications.Count == 0)
            {
                Console.WriteLine("No notifications have been sent yet.");
                return;
            }

            for (int i = 0; i < allNotifications.Count; i++)
            {
                Console.WriteLine($"\n#{i + 1}");
                Console.WriteLine(allNotifications[i]);
            }
        }
    }
}
