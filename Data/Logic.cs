using Microsoft.IdentityModel.Tokens;
using Mon.Models;
using Mon.Models.Dtos;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mon.Data
{
    public class Logic
    {

        public static string generarToKen(string user, string role, string claveSecreta)
        {
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            var Token = manejadorToken.WriteToken(token);

            return Token;

        }

        public RoleUser desifrarToken(string token)
        {
            var manejadorToken = new JwtSecurityTokenHandler();
            var jwtToken = manejadorToken.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return null;

            // Asegúrate de que estos ClaimTypes coincidan con los que usaste al crear el token
            var usuario = jwtToken?.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;
            var rol = jwtToken?.Claims.FirstOrDefault(c => c.Type == "role")?.Value;

            return new RoleUser
            {
                User = usuario,
                Role = rol // Usando rol como ejemplo
            };
        }

    }
}
