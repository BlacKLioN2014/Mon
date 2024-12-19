using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Mon.Data;
using Mon.Models;
using Mon.Models.Dtos;
using Mon.Repository.IRepository;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mon.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _Bd;
        private readonly UserManager<AppUsuario> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private string claveSecreta = string.Empty;
        private readonly IMapper _mapper;

        public UserRepository( ApplicationDbContext Bd, UserManager<AppUsuario> UserManager, RoleManager<IdentityRole> RoleManager, IConfiguration config, IMapper mapper)
        {
            _Bd = Bd;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
            _mapper = mapper;
        }

        public ICollection<AppUsuario> GetUsuarios()
        {
            return _Bd.AppUsuario.OrderBy(c => c.UserName).ToList();
        }

        public AppUsuario GetUsuario(string UsuarioId)
        {
            return _Bd.AppUsuario.FirstOrDefault(c => c.Id == UsuarioId);
        }

        public bool IsUniqueUser(string username)
        {
            var usernameBd = _Bd.AppUsuario.FirstOrDefault(u => u.UserName == username);

            if (usernameBd == null) 
            {
                return true;
            }
            return false;
        }

        public async Task<UserLoginRespuestaDto> Login(UserLoginDto UserLoginDto)
        {

            var usuario = _Bd.AppUsuario.FirstOrDefault(
                u => u.UserName.ToLower() == UserLoginDto.NombreUsuario.ToLower());

            bool isValid = await _UserManager.CheckPasswordAsync(usuario, UserLoginDto.Password);

            // validamos si el usuario no existe, con la combinacion de usuario y contraseña
            if (usuario == null || isValid == false)
            {
                return new UserLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null,
                    Role = null
                };
            }

            //Aqui existe usuario
            var roles = await _UserManager.GetRolesAsync(usuario);

            #region Codigo anterior

            //var manejadorToken = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(claveSecreta);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, usuario.UserName.ToString()),
            //        new Claim(ClaimTypes.Role, roles.FirstOrDefault())
            //    }),
            //    Expires = DateTime.UtcNow.AddMinutes(30),
            //    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = manejadorToken.CreateToken(tokenDescriptor);

            #endregion

            var token = Logic.generarToKen(usuario.UserName.ToString(), roles.FirstOrDefault(),claveSecreta); 

            UserLoginRespuestaDto usuarioLoginRespuesta = new UserLoginRespuestaDto()
            {
                Token = token,
                Usuario = usuario,
                Role = roles.FirstOrDefault()

            };

            return usuarioLoginRespuesta;
        }

        public async Task<UserDatosDto> Registro([FromBody] UserRegistroDto UserRegistroDto)
        {

            var usuarioExistente = _Bd.AppUsuario.FirstOrDefault(
                u => u.NormalizedEmail.ToLower() == UserRegistroDto.Correo.ToLower());

            if (usuarioExistente != null)
            {
                return new UserDatosDto();
            }

            AppUsuario usuario = new AppUsuario()
            {
                UserName = UserRegistroDto.NombreUsuario,
                Email = UserRegistroDto.Correo,
                NormalizedEmail = UserRegistroDto.NombreUsuario.ToUpper(),
                Nombre = UserRegistroDto.Nombre
            };

            //Validaciones
            var result = await _UserManager.CreateAsync(usuario, UserRegistroDto.Passsword);
            if (result.Succeeded)
            {
                if (!_RoleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
                {
                    await _RoleManager.CreateAsync(new IdentityRole("Admin"));
                    await _RoleManager.CreateAsync(new IdentityRole("User"));
                    await _UserManager.AddToRoleAsync(usuario, "Admin");

                    var usuarioRetornado = _Bd.AppUsuario.FirstOrDefault(u => u.UserName == UserRegistroDto.NombreUsuario);
                    return _mapper.Map<UserDatosDto>(usuarioRetornado);
                }
                else
                {
                    await _UserManager.AddToRoleAsync(usuario, "Admin");
                    var usuarioRetornado = _Bd.AppUsuario.FirstOrDefault(u => u.UserName == UserRegistroDto.NombreUsuario);
                    return _mapper.Map<UserDatosDto>(usuarioRetornado);
                }
            }

            return new UserDatosDto();
        }


    }
}
