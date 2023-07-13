using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class UserModel : UserWithoutPasswordModel
    {
        [Required(ErrorMessage = "Informe a Senha")]
        public string Password { get; set; } = string.Empty;
        public string PerfilDescription { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool ValidatePassword(string password) => Password == password;
    }
}