using Contacts.Models;
using Contacts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var users = _userRepository.GetUsers();

            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            var user = _userRepository.GetUserById(id);

            return View(user);
        }

        public IActionResult ConfirmRemove(int id)
        {
            var user = _userRepository.GetUserById(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var success = _userRepository.CheckIfEmailAlreadyExists(userModel);
                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.EmailAlreadyRegistered;
                        return View(userModel);
                    }

                    success = _userRepository.CheckIfUsernameAlreadyExists(userModel);
                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.UsernameAlreadyRegistered;
                        return View(userModel);
                    }

                    await _userRepository.CreateUserAsync(userModel);
                    TempData["SuccessMessage"] = MessagesModel.UserSuccesfulyCreated;

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"{MessagesModel.UnableToCreateUser} {ex.Message}";
                    return RedirectToAction("Index");
                }
            }

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserWithoutPasswordModel userWithoutPasswordModel)
        {
            UserModel? userModel = null;
            if (ModelState.IsValid)
            {
                try
                {
                    userModel = new UserModel
                    {
                        Id = userWithoutPasswordModel.Id,
                        Name = userWithoutPasswordModel.Name,
                        Email = userWithoutPasswordModel.Email,
                        Username = userWithoutPasswordModel.Username,
                        Perfil = userWithoutPasswordModel.Perfil
                    };

                    var success = _userRepository.CheckIfEmailAlreadyExists(userModel);
                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.EmailAlreadyRegistered;
                        return View(userModel);
                    }

                    success = _userRepository.CheckIfUsernameAlreadyExists(userModel);
                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.UsernameAlreadyRegistered;
                        return View(userModel);
                    }

                    await _userRepository.UpdateUserAsync(userModel);
                    TempData["SuccessMessage"] = MessagesModel.UserSuccesfulyUpdated;

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"{MessagesModel.UnableToUpdateUser} {ex.Message}";
                    return RedirectToAction("Index");
                }
            }

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _userRepository.RemoveUserAsync(id);
                TempData["SuccessMessage"] = MessagesModel.UserSuccesfulyRemoved;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToRemoveUser} {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}