using Embarcaciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public interface ICuentaService
    {
        Task<Cuenta> Login(string cuenta, string clave);
        // CRUD y consultas
        Task<Cuenta> Obtener(int id);
        Task<List<Cuenta>> ObtenerTodos(); // lista ejecutada
        Task<bool> Agregar(Cuenta cuenta);
        Task<bool> Actualizar(Cuenta cuenta);
        Task<bool> Eliminar(int id);
    }
}
