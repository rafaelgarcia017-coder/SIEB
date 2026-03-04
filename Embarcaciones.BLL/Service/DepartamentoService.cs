using Embarcaciones.DAL.Repositorio;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public class DepartamentoService:IDepartamentoService
    {
        private readonly IGenericRepositorio<Departamento> _DepartmentaRepo;
        public DepartamentoService(IGenericRepositorio<Departamento> departamentoRepo)
        {
            _DepartmentaRepo = departamentoRepo;
        }

        public async Task<bool> Actualizar(Departamento departamento)
        {
            return await _DepartmentaRepo.Actualizar(departamento);
        }

        public async Task<bool> Agregar(Departamento departamento)
        {
            return await _DepartmentaRepo.Agregar(departamento);
        }

        public async Task<bool> Eliminar(Departamento departamento)
        {
            return await _DepartmentaRepo.Eliminar(departamento);
        }

        public async Task<bool> ValidarDuplicados(string valor, int? id = null)
        {
            return await _DepartmentaRepo.ValidarDuplicados(valor, id);
        }

        public async Task<Departamento> Obtener(int id)
        {
            return await _DepartmentaRepo.Obtener(id);
        }

        public IQueryable<Departamento> ObtenerTodos()
        {
            return (IQueryable<Departamento>)_DepartmentaRepo.ObtenerTodos();
        }

        public async Task<bool> ValidarEliminar(int id)
        {
            return await _DepartmentaRepo.ValidarEliminar(id);
        }
    }
}
