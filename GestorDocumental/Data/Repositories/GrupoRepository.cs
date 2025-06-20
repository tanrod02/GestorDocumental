﻿using GestorDocumental.Data.Entities;
using GestorDocumental.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestorDocumental.Data.Repositories
{
    public class GrupoRepository :IGrupoRepository
    {
        private readonly IDbContextFactory<GestorDocumentalDbContext> _contextFactory;


        public GrupoRepository(IDbContextFactory<GestorDocumentalDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task AgregarGrupo(Grupos grupo)
        {

            try
            {
                using var context = _contextFactory.CreateDbContext();

                context.Grupos.Add(grupo);

                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener archivos de baja: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Grupos>> ObtenerGruposPorCurso(int codigoCurso)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                List<Grupos> grupos = await context.Grupos.Where(x => x.CodigoCurso == codigoCurso).ToListAsync();

                grupos = grupos.DistinctBy(x => x.Grupo).ToList();
                return grupos;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener archivos de baja: {ex.Message}");
                throw;
            }
        }

        public async Task<List<string>> ObtenerGrupos()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var grupos = await context.Grupos.Select(cu => cu.Grupo).Distinct().ToListAsync();

                return grupos;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los grupos: {ex.Message}");
                throw;
            }
        }
    }
}
