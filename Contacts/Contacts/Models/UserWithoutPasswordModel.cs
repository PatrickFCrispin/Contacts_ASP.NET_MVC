using Contacts.Enums;
using System.ComponentModel.DataAnnotations;

namespace Contacts.Models
{
    public class UserWithoutPasswordModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o E-mail")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o Usuário")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione o Perfil do usuário")]
        public PerfilEnum Perfil { get; set; }
    }
}