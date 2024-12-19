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

        public static ClaimsPrincipal? ValidarYDecodificarToken(string token, string claveSecreta)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false, // Configura esto según tu aplicación
                    ValidateAudience = false, // Configura esto según tu aplicación
                    ClockSkew = TimeSpan.Zero // Elimina margen de error para la expiración
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Verifica que el token sea un JWT
                if (validatedToken is JwtSecurityToken jwtToken &&
                    jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal; // Devuelve los claims del token
                }

                return null;
            }
            catch
            {
                // Si el token no es válido, devuelve null
                return null;
            }
        }

        public static UserRespuesta extraerToken(string token, string claveSecreta) 
        {
            var Respuesta = new UserRespuesta();

            var TimeExp = DateTime.Now;

            var claimsPrincipal = ValidarYDecodificarToken(token, claveSecreta);

            var manejadorToken = new JwtSecurityTokenHandler();
            // Decodifica el token
            var jwtToken = manejadorToken.ReadJwtToken(token);
            // Obtén el claim de expiración ("exp")
            var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);

            if (expClaim != null)
            {
                // La fecha de expiración está en formato UNIX timestamp
                var expTimestamp = long.Parse(expClaim.Value);
                TimeExp = DateTimeOffset.FromUnixTimeSeconds(expTimestamp).UtcDateTime;
            }

            if (claimsPrincipal != null)
            {
                Respuesta = new UserRespuesta()
                {
                    Usuario = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value,
                    Role = claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value,
                    Token = token,
                    time = TimeExp
                };
            }

            return Respuesta;

        }

    }
}
