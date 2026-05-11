using NotificationModelLibrary;

namespace NotificationDALLibrary.Interfaces
{
    public interface IUserRepository
    {
        int Add(User user);
        User? GetByEmail(string email);
        User? GetByPhone(string phoneNumber);
        List<User> GetAll();
    }
}
