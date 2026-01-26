using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public class DepartamentoService
    {
        private readonly IGenericRepositorio<Departamento> _DepartmentaRepo;
        public DepartamentoService(IGenericRepositorio<Departamento> departamentoRepo)
        {
            _DepartmentaRepo = departamentoRepo;
        }

        public async Task<bool> Actualizar(Departamento persona)
        {
            return await _DepartmentaRepo.Actualizar(persona);
        }

        public async Task<bool> Agregar(Departamento persona)
        {
            return await _DepartmentaRepo.Agregar(persona);
        }

        public async Task<bool> Eliminar(int id)
        {
            return await _DepartmentaRepo.Eliminar(id);
        }

        public async Task<Departamento> Obtener(int id)
        {
            return await _DepartmentaRepo.Obtener(id);
        }

        public IQueryable<Departamento> ObtenerTodos()
        {
            return (IQueryable<Departamento>)_DepartmentaRepo.ObtenerTodos();
        }
    }
}
