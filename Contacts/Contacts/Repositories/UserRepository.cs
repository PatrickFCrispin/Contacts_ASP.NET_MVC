using Contacts.Data;
using Contacts.Models;

namespace Contacts.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _dbContext;
        private const string Admin = "Administrador";
        private const string User = "Usuário";

        public UserRepository(DBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            try
            {
                return _dbContext.Users.ToList();
            }
            catch (Exception) { throw; }
        }

        public UserModel GetUserById(int id)
        {
            try
            {
                // Este método é chamado por operações e Views e quando chamado sei que o Id existe no banco de dados.
                // Logo, se por algum motivo o user retornar null, estouro uma excessão e não deixo o usuário seguir com a operação que chamou este método ou com a exibição na View para andamento da operação.
                return _dbContext.Users.First(x => x.Id == id);
            }
            catch (Exception) { throw; }
        }

        public UserModel GetUserByUsername(string username)
        {
            try
            {
                return _dbContext.Users.FirstOrDefault(x => x.Username == username);
            }
            catch (Exception) { throw; }
        }

        public bool CheckIfEmailAlreadyExists(UserModel userModel)
        {
            // Se Id > 0, então está editando um usuário já cadastrado.
            if (userModel.Id > 0)
            {
                foreach (var user in _dbContext.Users)
                {
                    if (user.Email == userModel.Email && user.Id != userModel.Id) { return true; }
                }

                return false;
            }

            foreach (var user in _dbContext.Users)
            {
                if (user.Email == userModel.Email) { return true; }
            }

            return false;
        }

        public bool CheckIfUsernameAlreadyExists(UserModel userModel)
        {
            if (userModel.Id > 0)
            {
                foreach (var user in _dbContext.Users)
                {
                    if (user.Username == userModel.Username && user.Id != userModel.Id) { return true; }
                }

                return false;
            }

            foreach (var user in _dbContext.Users)
            {
                if (user.Username == userModel.Username) { return true; }
            }

            return false;
        }

        public async Task CreateUserAsync(UserModel userModel)
        {
            try
            {
                userModel.PerfilDescription = userModel.Perfil switch
                {
                    Enums.PerfilEnum.Admin => Admin,
                    Enums.PerfilEnum.User => User,
                    _ => throw new Exception("Perfil de usuário não mapeado."),
                };
                userModel.CreatedAt = DateTime.Now;

                _dbContext.Users.Add(userModel);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task UpdateUserAsync(UserModel userModel)
        {
            try
            {
                var user = GetUserById(userModel.Id);

                user.Name = userModel.Name;
                user.Email = userModel.Email;
                user.Username = userModel.Username;
                user.Perfil = userModel.Perfil;
                user.PerfilDescription = user.Perfil switch
                {
                    Enums.PerfilEnum.Admin => Admin,
                    Enums.PerfilEnum.User => User,
                    _ => throw new Exception("Perfil de usuário não mapeado."),
                };
                user.UpdatedAt = DateTime.Now;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task RemoveUserAsync(int id)
        {
            try
            {
                var user = GetUserById(id);

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception) { throw; }
        }
    }
}