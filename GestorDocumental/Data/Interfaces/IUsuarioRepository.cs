using GestorDocumental.Data.Entities;

namespace GestorDocumental.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<bool> RegistrarUsuarioAsync(Usuario usuario);
        Task<Usuario?> ObtenerUsuarioPorCorreoAsync(string correo);

        Task<List<Usuario>> ObtenerUsuariosPorGrupo(int codigoCurso, string Grupo);
    }
}
