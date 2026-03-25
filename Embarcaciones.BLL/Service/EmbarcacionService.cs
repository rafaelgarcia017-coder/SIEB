using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public class EmbarcacionService : IEmbarcacionService
    {
        private readonly IEmbarcacionRepositorio _EmbarcacionRepo;
        public EmbarcacionService(IEmbarcacionRepositorio embarcacionRepo)
        {
            _EmbarcacionRepo = embarcacionRepo;
        }
        public Task<bool> Actualizar(Embarcacion departamento)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Agregar(Embarcacion departamento)
        {
            return await _EmbarcacionRepo.Agregar(departamento);
        }

        public Task<bool> Eliminar(Embarcacion departamento)
        {
            throw new NotImplementedException();
        }

        public Task<Embarcacion> Obtener(int id)
        {
            return _EmbarcacionRepo.Obtener(id);
        }

        public Task<bool> ValidarDuplicados(string valor, int? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarEliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmbarcacionDTO>> ObtenerTodos()
        {
            return await _EmbarcacionRepo.ObtenerTodos();
        }
    }
}
