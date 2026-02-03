using Embarcaciones.AplicacionWeb.Models.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using modelo = Embarcaciones.Models;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Catalogos.Municipio
{
    public class MunicipioVM:FormViewModelBase
    {
        public int IdMunicipio { get; set; }
        [Required(ErrorMessage = "Por Favor Seleccione un Departamento")]
        public int IdDepartamento { get; set; }

        [Required(ErrorMessage = "Por Favor Ingrese un Municipio")]
        public string NombreMunicipio { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public List<modelo.Departamento> ListaDepartamentos { get; set; }


    }
}
