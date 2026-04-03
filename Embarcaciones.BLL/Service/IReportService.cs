using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embarcaciones.BLL.Service
{
    public interface IReportService
    {
        /// <summary>
        /// Construye la URL del reporte SSRS según parámetros
        /// </summary>
        /// <param name="reportName">Nombre del reporte</param>
        /// <param name="parametros">Parámetros del reporte</param>
        /// <param name="format">Formato de salida (HTML5, PDF, Excel)</param>
        string GetReportUrl(string reportName, string parametros = null, string format = "HTML5");

        /// <summary>
        /// Descarga el reporte como byte array (PDF/Excel)
        /// </summary>
        /// <param name="reportName">Nombre del reporte</param>
        /// <param name="parametros">Parámetros del reporte</param>
        /// <param name="format">Formato de salida (PDF, Excel)</param>
        Task<byte[]> DownloadReportAsync(string reportName, string parametros, string format = "PDF");
    }
}
