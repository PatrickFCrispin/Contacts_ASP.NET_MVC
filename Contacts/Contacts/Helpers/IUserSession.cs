using Contacts.Models;

namespace Contacts.Helpers
{
    public interface IUserSession
    {
        void CreateUserSession(UserModel userModel);
        UserModel GetUserSession();
        void RemoveUserSession();
    }
}