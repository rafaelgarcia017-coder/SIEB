using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public  class MunicipioService : IMunicipioService
    {
        private readonly IGenericRepositorio<Municipio> _MunicipioRepo;

        public MunicipioService(IGenericRepositorio<Municipio> municipio)
        {
            _MunicipioRepo = municipio;
        }
        public async Task<bool> Actualizar(Municipio municipio)
        {
            return await _MunicipioRepo.Actualizar(municipio);
        }

        public async Task<bool> Agregar(Municipio municipio)
        {
            return await _MunicipioRepo.Agregar(municipio);
        }

        public async Task<bool> Eliminar(Municipio  municipio)
        {
            return await _MunicipioRepo.Eliminar(municipio);
        }

        public async Task<Municipio> Obtener(int id)
        {
            return await _MunicipioRepo.Obtener(id);
        }

        public IQueryable<Municipio> ObtenerTodos()
        {
            return (IQueryable<Municipio>)_MunicipioRepo.ObtenerTodos();
        }

        public async Task<bool> ValidarDuplicados(string valor, int? id = null)
        {
            return await _MunicipioRepo.ValidarDuplicados(valor,id);
        }
        public async Task<bool> ValidarEliminar(int id)
        {
            return await _MunicipioRepo.ValidarEliminar(id);
        }

    }
}
