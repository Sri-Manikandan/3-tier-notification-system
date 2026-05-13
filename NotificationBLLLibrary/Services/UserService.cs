using NotificationBLLLibrary.Interfaces;
using NotificationDALLibrary.Interfaces;
using NotificationDALLibrary.Repositories;
using NotificationModelLibrary;

namespace NotificationBLLLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public int GetOrAddUser(User user, NotificationType type)
        {
            User? existing = type == NotificationType.Email ? _userRepository.GetByEmail(user.Email) : _userRepository.GetByPhone(user.PhoneNumber);

            if (existing != null){
                user.Id = existing.Id;
                return existing.Id;
            }

            return _userRepository.Add(user).Id;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }
    }
}
