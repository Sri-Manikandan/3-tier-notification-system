using System;
using System.Collections.Generic;
using System.Linq;
using NotificationBLLLibrary.Services;
using NotificationModelLibrary;
using NotificationModelLibrary.Exceptions;

namespace NotificationFEApplication
{
    public class Program
    {
        private static NotificationService _notificationService = new NotificationService();
        private static UserService _userService = new UserService();

        public static void Main()
        {
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
                        case "1": SendNotificationFlow(); break;
                        case "2": ViewAllNotifications(); break;
                        case "3": ViewAllUsers(); break;
                        case "4": ViewNotificationsByUser(); break;
                        case "5":
                            run = false;
                            Console.WriteLine("Goodbye!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select a number from 1 to 5.");
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
            Console.WriteLine("Simple Notification System (3-Tier)");
            Console.WriteLine("1. Send notification");
            Console.WriteLine("2. View all notifications");
            Console.WriteLine("3. View all users");
            Console.WriteLine("4. View notifications by user");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");
        }

        private static void SendNotificationFlow()
        {
            Console.WriteLine("Send Notification");

            Console.Write("Enter name: ");
            string name = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("Enter email: ");
            string email = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("Enter phone (10 digits): ");
            string phone = (Console.ReadLine() ?? string.Empty).Trim();

            Console.WriteLine("Notification type: [1] Email  [2] SMS");
            Console.Write("Enter choice: ");
            string? typeChoice = Console.ReadLine()?.Trim();

            NotificationType type;
            switch (typeChoice)
            {
                case "1": type = NotificationType.Email; break;
                case "2": type = NotificationType.SMS; break;
                default:
                    Console.WriteLine("Invalid notification type.");
                    return;
            }

            Console.Write("Enter message: ");
            string message = Console.ReadLine() ?? string.Empty;

            User user = new User(name, email, phone);

            Console.WriteLine("\nSending...");
            _notificationService.SendNotification(message, type, user);

            string target = type == NotificationType.Email ? email : phone;
            Console.WriteLine($"✓ Notification sent successfully to {target}");
        }

        private static void ViewAllNotifications()
        {
            Console.WriteLine("All Notifications");
            List<Notification> notifications = _notificationService.GetAllNotifications();

            if (notifications.Count == 0)
            {
                Console.WriteLine("No notifications found.");
                return;
            }

            for (int i = 0; i < notifications.Count; i++)
                Console.WriteLine($"{i + 1}. {notifications[i]}");
        }

        private static void ViewAllUsers()
        {
            Console.WriteLine("All Users");
            List<User> users = _userService.GetAllUsers();

            if (users.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }

            PrintUsersTable(users);
        }

        private static void ViewNotificationsByUser()
        {
            Console.WriteLine("Notifications by User");
            List<User> users = _userService.GetAllUsers();

            if (users.Count == 0)
            {
                Console.WriteLine("No users found.");
                return;
            }

            PrintUsersTable(users);

            Console.Write("\nSelect user (enter number): ");
            string? input = Console.ReadLine()?.Trim();

            if (!int.TryParse(input, out int index) || index < 1 || index > users.Count)
            {
                Console.WriteLine("Invalid selection.");
                return;
            }

            User selected = users[index - 1];
            List<Notification> userNotifications = _notificationService
                .GetAllNotifications()
                .Where(n => n.RecipientId == selected.Id)
                .ToList();

            Console.WriteLine($"\nNotifications for {selected.Name}");

            if (userNotifications.Count == 0)
            {
                Console.WriteLine($"No notifications found for {selected.Name}.");
                return;
            }

            for (int i = 0; i < userNotifications.Count; i++)
                Console.WriteLine($"{i + 1}. {userNotifications[i]}");
        }

        private static void PrintUsersTable(List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
                Console.WriteLine($"{i + 1}. {users[i]}");
        }
    }
}
