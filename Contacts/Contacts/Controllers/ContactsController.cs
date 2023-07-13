using Contacts.Models;
using Contacts.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            var contacts = _contactRepository.GetContacts();

            return View(contacts);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            var contact = _contactRepository.GetContactById(id);

            return View(contact);
        }

        public IActionResult ConfirmRemove(int id)
        {
            var contact = _contactRepository.GetContactById(id);

            return View(contact);
        }

        public ContactModel Details(int id)
        {
            return _contactRepository.GetContactById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var success = _contactRepository.CheckIfEmailAlreadyExists(contactModel);
                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.EmailAlreadyRegistered;
                        return View(contactModel);
                    }

                    success = _contactRepository.CheckIfPhoneAlreadyExists(contactModel);
                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.PhoneAlreadyRegistered;
                        return View(contactModel);
                    }

                    await _contactRepository.CreateContactAsync(contactModel);
                    TempData["SuccessMessage"] = MessagesModel.ContactSuccesfulyCreated;

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"{MessagesModel.UnableToCreateContact} {ex.Message}";
                    return RedirectToAction("Index");
                }
            }

            return View(contactModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var success = _contactRepository.CheckIfEmailAlreadyExists(contactModel);
                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.EmailAlreadyRegistered;
                        return View(contactModel);
                    }

                    success = _contactRepository.CheckIfPhoneAlreadyExists(contactModel);
                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.PhoneAlreadyRegistered;
                        return View(contactModel);
                    }

                    await _contactRepository.UpdateContactAsync(contactModel);
                    TempData["SuccessMessage"] = MessagesModel.ContactSuccesfulyUpdated;

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"{MessagesModel.UnableToUpdateContact} {ex.Message}";
                    return RedirectToAction("Index");
                }
            }

            return View(contactModel);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _contactRepository.RemoveContactAsync(id);
                TempData["SuccessMessage"] = MessagesModel.ContactSuccesfulyRemoved;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToRemoveContact} {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}