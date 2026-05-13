using NotificationDALLibrary.Interfaces;
using NotificationModelLibrary;

namespace NotificationDALLibrary.Repositories
{
    public class UserRepository : AbstractRepository<User>, IUserRepository
    {
        public User? GetByEmail(string email)
        {
            return context.Users.Where(x => x.Email == email).FirstOrDefault();
        }

        public User? GetByPhone(string phoneNumber)
        {
            return context.Users.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefault();
        }
    }
}
