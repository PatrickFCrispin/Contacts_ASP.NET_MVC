using Contacts.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Contacts.ViewComponents
{
    public class Menu : ViewComponent
    {
        // O nome do método precisa ser 'Invoke' ou 'InvokeAsync', pois é lincado com o Component de _Layout.cshtml -> @await Component.InvokeAsync("Menu")
        // Chamo esta View Component no _Layout.cshtml, pois é o layout base de toda a minha aplicação, então independente se estou 
        // na tela de Contatos ou de Usuários, o Menu é invocado na _Layout.cshtml e então carregado na tela.
        public IViewComponentResult Invoke()
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            if (string.IsNullOrEmpty(userSession)) { return null; }

            var userModel = JsonConvert.DeserializeObject<UserModel>(userSession);

            return View("Menu", userModel);
        }
    }
}