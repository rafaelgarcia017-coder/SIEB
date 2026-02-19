using Embarcaciones.AplicacionWeb.Models.Utils;

namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Embarcaciones
{
    public class EmbarcacionesVM:FormViewModelBase
    {
        public PropietarioEmbarcacionesVM EmbarcacionPropietario { get; set; } = new();

        public DatosEmbarcacionesVM DatosEmbarcaciones { get; set; } = new();
    }
}
