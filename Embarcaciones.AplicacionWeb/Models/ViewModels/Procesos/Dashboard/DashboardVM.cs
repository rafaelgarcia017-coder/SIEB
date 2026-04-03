
namespace Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Dashboard
{
    public class DashboardVM
    {
        public int TotalEmbarcaciones { get; set; }
        public int TotalPersonas { get; set; }
        public int TotalPuertos { get; set; }
        public int TotalNacionalidades { get; set; }

        // Datos de gráficas
        public List<TipoEmbarcacionVM> TiposEmbarcacion { get; set; } = new();
        public List<RegistroMesVM> RegistrosMes { get; set; } = new ();

        // Subclases internas para tipado fuerte
        public class TipoEmbarcacionVM
        {
            public string Tipo { get; set; }
            public int Cantidad { get; set; }
        }

        public class RegistroMesVM
        {
            public int Mes { get; set; }
            public int Cantidad { get; set; }
        }
    }
}
