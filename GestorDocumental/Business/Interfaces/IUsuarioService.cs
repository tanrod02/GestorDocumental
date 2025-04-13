using GestorDocumental.Data.Entities;
using System.Threading.Tasks;

namespace GestorDocumental.Business.Interfaces
{
    public interface IUsuarioService
    {
        Task<bool> RegistrarUsuarioAsync(Usuario usuario);
        Task<Usuario?> IniciarSesionAsync(string correo, string contraseña);

        Task<List<Usuario>> ObtenerUsuariosPorGrupo(int codigoCurso, string Grupo);


    }
}
