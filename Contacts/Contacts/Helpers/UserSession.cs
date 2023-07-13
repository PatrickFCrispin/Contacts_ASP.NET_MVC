using Contacts.Models;
using Newtonsoft.Json;

namespace Contacts.Helpers
{
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string SessionName = "UserSession";

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void CreateUserSession(UserModel userModel)
        {
            var value = JsonConvert.SerializeObject(userModel);
            _httpContextAccessor.HttpContext?.Session.SetString(SessionName, value);
        }

        public UserModel GetUserSession()
        {
            var userSession = _httpContextAccessor.HttpContext?.Session.GetString(SessionName);
            return string.IsNullOrEmpty(userSession) ? null : JsonConvert.DeserializeObject<UserModel>(userSession);
        }

        public void RemoveUserSession() => _httpContextAccessor.HttpContext?.Session.Remove(SessionName);
    }
}