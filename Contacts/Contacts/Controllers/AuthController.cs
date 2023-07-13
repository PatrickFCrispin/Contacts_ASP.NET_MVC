using Contacts.Helpers;
using Contacts.Models;
using Contacts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;

        public AuthController(IUserRepository userRepository, IUserSession userSession)
        {
            _userRepository = userRepository;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            return _userSession.GetUserSession() is not null ? RedirectToAction("Index", "Contacts") : View();
        }

        [HttpPost]
        public IActionResult AttemptToLogin(AuthModel authModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userRepository.GetUserByUsername(authModel.Username);
                    if (user is not null)
                    {
                        if (user.ValidatePassword(authModel.Password))
                        {
                            _userSession.CreateUserSession(user);
                            return RedirectToAction("Index", "Contacts");
                        }
                    }

                    TempData["ErrorMessage"] = $"{MessagesModel.InvalidCredentials}";
                    return View("Index", authModel);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"{MessagesModel.UnableToLogin} {ex.Message}";
                    return RedirectToAction("Index");
                }
            }

            return View("Index");
        }

        public IActionResult Logout()
        {
            _userSession.RemoveUserSession();
            return RedirectToAction("Index", "Auth");
        }
    }
}