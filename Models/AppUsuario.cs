using Microsoft.AspNetCore.Identity;

namespace Mon.Models
{
    public class AppUsuario : IdentityUser
    {
        public string Nombre { get; set; }
    }
}
