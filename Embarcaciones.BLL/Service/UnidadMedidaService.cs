using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly IUnidadMedidaRepositorio _unidadMedidaRepo;
        public UnidadMedidaService(IUnidadMedidaRepositorio unidadMedidaRepo)
        {
            _unidadMedidaRepo = unidadMedidaRepo;
        }
        public async Task<bool> Actualizar(UnidadMedida modelo)
        {
            return await _unidadMedidaRepo.Actualizar(modelo);
        }

        public async Task<bool> Agregar(UnidadMedida modelo)
        {
            return await _unidadMedidaRepo.Agregar(modelo);
        }

        public async Task<bool> Eliminar(UnidadMedida modelo)
        {
            return await _unidadMedidaRepo.Eliminar(modelo);
        }

        public async Task<UnidadMedida> Obtener(int id)
        {
            return await _unidadMedidaRepo.Obtener(id);
        }

        public  IQueryable<UnidadMedida> ObtenerTodos()
        {
            return (IQueryable<UnidadMedida>)_unidadMedidaRepo.ObtenerTodos();
        }

        public async Task<bool> ValidarUnidadDuplicadas(string unidadmedidad, string? abreviatura, int? idUnidadMedida)
        {
            return await _unidadMedidaRepo.ValidarUnidadDuplicadas(unidadmedidad,abreviatura,idUnidadMedida);
        }
    }
}
