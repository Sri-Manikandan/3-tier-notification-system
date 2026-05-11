using NotificationModelLibrary;

namespace NotificationBLLLibrary.Interfaces
{
    public interface IUserService
    {
        int GetOrAddUser(User user, NotificationType type);
        List<User> GetAllUsers();
    }
}
