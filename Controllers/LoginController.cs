using Microsoft.AspNetCore.Mvc;
using Mon.Models.Dtos;
using Mon.Repository;
using Mon.Repository.IRepository;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Mon.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUserRepository _userRepository;

        public LoginController( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View(new UserRegistroDto());
        }

        public async Task<JsonResult> Validate([FromBody] UserLoginDto usuarioLoginDto)
        {
            UserLoginRespuestaDto userLoginRespuestaDto = await _userRepository.Login(usuarioLoginDto);
            return Json(userLoginRespuestaDto);
        }

        public async Task<JsonResult> Registrar_Usuario([FromBody] UserRegistroDto UserRegistroDto)
        {
            Models.Response<string> A = new Models.Response<string>();

            if (!ModelState.IsValid)
            {
                A.success = false;
                A.message = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un carácter especial.";
                return Json(A);
            }

            UserDatosDto userDatosDto = await _userRepository.Registro(UserRegistroDto);

            if(userDatosDto.Username == null)
            {
                A.success = false;
                A.message = "El correo ya esta registrado";
                return Json(A);
            }
            else
            {
                A.success = true;
                A.message = "Usuario registrado correctamente";
                return Json(A);
            }

        }

    }
}
