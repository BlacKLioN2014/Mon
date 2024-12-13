using Mon.Models;
using Mon.Models.Dtos;

namespace Mon.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<AppUsuario> GetUsuarios();


        AppUsuario GetUsuario(string UsuarioId);


        bool IsUniqueUser(string Usuario);

        Task<UserLoginRespuestaDto> Login(UserLoginDto usuarioLoginDto);


        Task<UserDatosDto> Registro(UserRegistroDto UserRegistroDto);


    }
}
