using NotificationModelLibrary;

namespace NotificationDALLibrary.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByEmail(string email);
        User? GetByPhone(string phoneNumber);
    }
}
