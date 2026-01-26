using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class DepartamentoRepositorio : IGenericRepositorio<Departamento>
    {
        private readonly EmbarcacionesBDContext _dbcontext;
        public DepartamentoRepositorio(EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }

        public  async Task<bool> Actualizar(Departamento modelo)
        {
            _dbcontext.Departamentos.Update(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Agregar(Departamento modelo)
        {
            _dbcontext.Departamentos.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Departamento> Obtener(int id)
        {
            return await _dbcontext.Departamentos.FindAsync(id);
        }

        public  IQueryable<Departamento> ObtenerTodos()
        {
            IQueryable<Departamento> queryDepartamento = _dbcontext.Departamentos;
            return queryDepartamento;
        }
    }
}
