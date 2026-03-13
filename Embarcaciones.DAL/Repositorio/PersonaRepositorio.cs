using Embarcaciones.DAL.DataContext;
using Embarcaciones.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.DAL.Repositorio
{
    public class PersonaRepositorio : IPersonaRepositorio
    {
        private readonly EmbarcacionesBDContext _dbcontext;
        public PersonaRepositorio(EmbarcacionesBDContext context)
        {
            _dbcontext = context;
        }
        public async Task<bool> Actualizar(Persona modelo)
        {
            _dbcontext.Persona.Attach(modelo);

            _dbcontext.Entry(modelo).Property(x => x.NombreCompleto).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Direccion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Correo).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Telefono).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdTipoIdentificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.Identificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Agregar(Persona modelo)
        {
            _dbcontext.Persona.Add(modelo);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Eliminar(Persona modelo)
        {
         
            _dbcontext.Persona.Attach(modelo);
            _dbcontext.Entry(modelo).Property(x => x.EstaActivo).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.EsHistorico).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.FechaModificacion).IsModified = true;
            _dbcontext.Entry(modelo).Property(x => x.IdUsuarioModificacion).IsModified = true;
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Persona> Obtener(int id)
        {
            return await _dbcontext.Persona.FindAsync(id);
        }

        public IQueryable<Persona> ObtenerTodos()
        {
            IQueryable<Persona> queryPersona = _dbcontext.Persona
                                                                                  .Include(d => d.UsuarioCreacionNavigation)
                                                                                  .Include(d => d.UsuarioModificacionNavigation)
                                                                                  .Include(i => i.TipoIdentificacionNavigation)
                                                                                  .Where(w => (bool)w.EstaActivo);
            return queryPersona;
        }

        public async Task<bool> ValidarPersonasDuplicadas(string nombreCompleto, string identificacion, int? idTipoIdentificacion, int? id)
        {

            var nombreNormalizado = nombreCompleto?.Trim().ToUpper();
            var identificacionTrim = identificacion?.Trim();

            // 1️⃣ Solo por nombre
            if (!string.IsNullOrWhiteSpace(nombreNormalizado) &&
                string.IsNullOrWhiteSpace(identificacionTrim) &&
                !idTipoIdentificacion.HasValue)
            {
                return await _dbcontext.Persona.AnyAsync(a =>
                    a.EstaActivo == true &&
                    a.NombreCompleto.Trim().ToUpper() == nombreNormalizado &&
                    (!id.HasValue || a.IdPersona != id.Value)
                );
            }
            // 2️⃣ Nombre + tipo de identificación (sin identificación)
            else if (!string.IsNullOrWhiteSpace(nombreNormalizado) &&
                     idTipoIdentificacion.HasValue &&
                     string.IsNullOrWhiteSpace(identificacionTrim))
            {
                return await _dbcontext.Persona.AnyAsync(a =>
                    a.EstaActivo == true &&
                    a.NombreCompleto.Trim().ToUpper() == nombreNormalizado &&
                    a.IdTipoIdentificacion == idTipoIdentificacion.Value &&
                    (!id.HasValue || a.IdPersona != id.Value)
                );
            }
            // 3️⃣ Nombre + tipo + identificación
            else if (!string.IsNullOrWhiteSpace(nombreNormalizado) &&
                     idTipoIdentificacion.HasValue &&
                     !string.IsNullOrWhiteSpace(identificacionTrim))
            {
                return await _dbcontext.Persona.AnyAsync(a =>
                    a.EstaActivo == true &&
                    a.NombreCompleto.Trim().ToUpper() == nombreNormalizado &&
                    a.IdTipoIdentificacion == idTipoIdentificacion.Value &&
                    a.Identificacion == identificacionTrim &&
                    (!id.HasValue || a.IdPersona != id.Value)
                );
            }

            // 4️⃣ Caso sin nada que validar
            return false;

        }
        public async Task<bool> ValidarEliminar(int id)
        {
            var connectionString = _dbcontext.Database.GetDbConnection().ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand("Fundaciones.PermiteEliminarPersona", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@IdPersona", SqlDbType.Int).Value = id;

                await connection.OpenAsync();

                var result = await command.ExecuteScalarAsync();

                return result != null && Convert.ToBoolean(result);
            }
        }

    }
}
