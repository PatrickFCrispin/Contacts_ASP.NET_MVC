using Contacts.Data;
using Contacts.Models;

namespace Contacts.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly DBContext _dbContext;

        public ContactRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IEnumerable<ContactModel> GetContacts()
        {
            try
            {
                return _dbContext.Contacts.ToList();
            }
            catch (Exception) { throw; }
        }

        public ContactModel GetContactById(int id)
        {
            try
            {
                // Este método é chamado por operações e Views e quando chamado sei que o Id existe no banco de dados.
                // Logo, se por algum motivo o contact retornar null, estouro uma excessão e não deixo o usuário seguir com a operação que chamou este método ou com a exibição na View para andamento da operação.
                return _dbContext.Contacts.First(x => x.Id == id);
            }
            catch (Exception) { throw; }
        }

        public bool CheckIfEmailAlreadyExists(ContactModel contactModel)
        {
            // Se Id > 0, então está editando um contato já cadastrado.
            if (contactModel.Id > 0)
            {
                foreach (var contact in _dbContext.Contacts)
                {
                    if (contact.Email == contactModel.Email && contact.Id != contactModel.Id) { return true; }
                }

                return false;
            }

            foreach (var contact in _dbContext.Contacts)
            {
                if (contact.Email == contactModel.Email) { return true; }
            }

            return false;
        }

        public bool CheckIfPhoneAlreadyExists(ContactModel contactModel)
        {
            if (contactModel.Id > 0)
            {
                foreach (var contact in _dbContext.Contacts)
                {
                    if (contact.Phone == contactModel.Phone && contact.Id != contactModel.Id) { return true; }
                }

                return false;
            }

            foreach (var contact in _dbContext.Contacts)
            {
                if (contact.Phone == contactModel.Phone) { return true; }
            }

            return false;
        }

        public async Task CreateContactAsync(ContactModel contactModel)
        {
            try
            {
                contactModel.CreatedAt = DateTime.Now;

                _dbContext.Contacts.Add(contactModel);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateContactAsync(ContactModel contactModel)
        {
            try
            {
                var contact = GetContactById(contactModel.Id);

                contact.Name = contactModel.Name;
                contact.Email = contactModel.Email;
                contact.Phone = contactModel.Phone;
                contact.UpdatedAt = DateTime.Now;

                _dbContext.Contacts.Update(contact);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task RemoveContactAsync(int id)
        {
            try
            {
                var contact = GetContactById(id);

                _dbContext.Contacts.Remove(contact);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }
    }
}