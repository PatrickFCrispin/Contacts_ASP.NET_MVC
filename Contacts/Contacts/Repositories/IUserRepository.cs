using Contacts.Models;

namespace Contacts.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUserById(int id);
        UserModel GetUserByUsername(string username);
        bool CheckIfEmailAlreadyExists(UserModel userModel);
        bool CheckIfUsernameAlreadyExists(UserModel userModel);
        Task CreateUserAsync(UserModel userModel);
        Task UpdateUserAsync(UserModel userModel);
        Task RemoveUserAsync(int id);
    }
}