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

        public async Task<bool> Eliminar(int id)
        {
            return await _MunicipioRepo.Eliminar(id);
        }

        public async Task<Municipio> Obtener(int id)
        {
            return await _MunicipioRepo.Obtener(id);
        }

        public IQueryable<Municipio> ObtenerTodos()
        {
            return (IQueryable<Municipio>)_MunicipioRepo.ObtenerTodos();
        }

        public async Task<bool> ValidarDuplicados(string valor)
        {
            return await _MunicipioRepo.ValidarDuplicados(valor);
        }
    }
}
