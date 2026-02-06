using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class CatalogoValorRepositorio : ICatalogoValorRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;

        public CatalogoValorRepositorio(EmbarcacionesBDContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Actualizar(CatalogoValor modelo)
        {
            _dbcontext.CatalogoValors.Attach(modelo);

            _dbcontext.Entry(modelo).Property(x => x.Nombre).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Descripcion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;

            await _dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Agregar(CatalogoValor modelo)
        {
            _dbcontext.CatalogoValors.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CatalogoValor> Obtener(int id)
        {
            return await _dbcontext.CatalogoValors.FindAsync(id);
        }

        public IQueryable<CatalogoValor> ObtenerTodos(int idCatalogo)
        {
            IQueryable<CatalogoValor> queryCatalogo = _dbcontext.CatalogoValors
                                                                      .Where(w=> w.IdCatalogo == idCatalogo)
                                                                     .Include(d => d.UsuarioCreacionNavigation)
                                                                     .Include(d => d.UsuarioModificacionNavigation);

            return queryCatalogo;
        }

        public async Task<bool> ValidarCatalogo(int idCatalogo, string valor)
        {
            return await _dbcontext.CatalogoValors
                                      .AnyAsync(a =>a.Nombre == valor && a.IdCatalogo == idCatalogo);
        }


    }
}
