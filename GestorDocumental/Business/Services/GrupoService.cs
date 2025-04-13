using GestorDocumental.Business.Interfaces;
using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;

namespace GestorDocumental.Business.Services
{
    public class GrupoService :IGrupoService
    {

        private readonly IGrupoRepository _grupoRepository;

        public GrupoService(IGrupoRepository grupoRepository)
        {
            _grupoRepository = grupoRepository;
        }

        public async Task AgregarGrupo(Grupos grupo)
        {
            await _grupoRepository.AgregarGrupo(grupo);
        }

        public async Task<List<Grupos>> ObtenerGruposPorCurso(int codigoCurso)
        {
            return await _grupoRepository.ObtenerGruposPorCurso(codigoCurso);
        }

    }
}
