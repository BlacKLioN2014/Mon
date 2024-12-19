using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Mon.Models.Dtos
{
    public class UserLoginRespuestaDto
    {

        public AppUsuario Usuario { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        
    }
}
