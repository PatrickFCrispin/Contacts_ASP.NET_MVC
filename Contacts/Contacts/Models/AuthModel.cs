using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class AuthModel
    {
        [Required(ErrorMessage = "Informe o Usuário")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a Senha")]
        public string Password { get; set; } = string.Empty;
    }
}