using Contacts.Models;

namespace Contacts.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<ContactModel> GetContacts();
        ContactModel GetContactById(int id);
        bool CheckIfEmailAlreadyExists(ContactModel contactModel);
        bool CheckIfPhoneAlreadyExists(ContactModel contactModel);
        Task CreateContactAsync(ContactModel contactModel);
        Task UpdateContactAsync(ContactModel contactModel);
        Task RemoveContactAsync(int id);
    }
}