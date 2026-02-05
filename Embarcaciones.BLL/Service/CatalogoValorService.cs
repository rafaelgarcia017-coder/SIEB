using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public class CatalogoValorService : ICatalogoValorService
    {
        private readonly ICatalogoValorRepositorio _catValorRepositorio;

        public CatalogoValorService(ICatalogoValorRepositorio catValorRepositorio)
        {
           _catValorRepositorio = catValorRepositorio;
        }
        public async Task<bool> Actualizar(CatalogoValor entity)
        {
            return await _catValorRepositorio.Actualizar(entity);
        }

        public async Task<bool> Agregar(CatalogoValor entity)
        {
            return await _catValorRepositorio.Agregar(entity);
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CatalogoValor> Obtener(int id)
        {
            return await _catValorRepositorio.Obtener(id);
        }

        public IQueryable <CatalogoValor> ObtenerTodos(int idCatalogo)
        {
            return  _catValorRepositorio.ObtenerTodos(idCatalogo);
        }

        public async Task<bool> ValidarCatalogo(int idCatalogo, string valor)
        {
            return await _catValorRepositorio.ValidarCatalogo (idCatalogo,valor);
        }
    }
}
