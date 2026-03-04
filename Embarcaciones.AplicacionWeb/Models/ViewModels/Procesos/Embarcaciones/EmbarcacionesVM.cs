using Embarcaciones.AplicacionWeb.Models.Utils;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Embarcaciones
{
    public class EmbarcacionesVM:FormViewModelBase
    {
        public PropietarioEmbarcacionesVM Propietario { get; set; } = new();

        public DatosEmbarcacionesVM Embarcacion { get; set; } = new();

        public ConstruccionEmbarcacionesVM Construccion { get; set; } = new();
    }
}
