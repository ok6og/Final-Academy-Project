using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User?>> GetAllUsers();
        Task<User?> GetUserById(int userId);
        Task<User?> AddUser(User user);
        Task<User?> UpdateUser(User user);
        Task<User?> DeleteUser(int userId);
        Task<bool> DoesUserHaveSubscription(int userId);
    }
}
