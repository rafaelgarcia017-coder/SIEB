using Embarcaciones.AplicacionWeb.Models.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Embarcaciones.AplicacionWeb.Controllers
{
    public class CustomController :Controller
    {
        public List<string> Errores { get; }
        public List<string> Advertencia { get; }

        public CustomController()
        {
            Errores = new List<string>();
            Advertencia = new List<string>();
        }

        public void AddError(string error)
        {
            if (string.IsNullOrEmpty(error)) return;


            Errores.Add(error.CleanString(StringUtils.CommonAllowedChars));

            TempData[Notificacion.Error] = Errores.Any()
                ? Errores.Aggregate((msjA, msjB) => msjA + "-" + msjB)
                : string.Empty;
        }

        public void AddAdvertencia(string advertencia)
        {
            if (string.IsNullOrEmpty(advertencia)) return;
            Advertencia.Add(advertencia.CleanString(StringUtils.CommonAllowedChars));
            TempData[Notificacion.Advertencia] = Advertencia.Any()
                ? Advertencia.Aggregate((msjA, msjB) => msjA + "-" + msjB)
                : string.Empty;
        }

        public void AddExito(string exito)
        {
            if (string.IsNullOrEmpty(exito)) return;

            exito = exito.CleanString(StringUtils.CommonAllowedChars);

            if (TempData.ContainsKey(Notificacion.Exito))
                TempData[Notificacion.Exito] = exito;
            else
                TempData.Add(Notificacion.Exito, exito);

            TempData.Keep();
        }
        public void AddInfo(string info)
        {
            if (string.IsNullOrEmpty(info)) return;

            info = info.CleanString(StringUtils.CommonAllowedChars);

            if (TempData.ContainsKey(Notificacion.Info))
                TempData[Notificacion.Info] = info;
            else
                TempData.Add(Notificacion.Info, info);

            TempData.Keep();
        }

        public Mensaje ErroresFromModel()
        {
            var errorList = new List<string>();
            foreach (var modelStateDD in ModelState)
            {
                var key = modelStateDD.Key;
                var fieldState = modelStateDD.Value;

                if (!fieldState.Errors.Any()) continue;

                errorList.Add(
                    $"{key}: {fieldState.Errors.Select(x => x.ErrorMessage).Aggregate((a, b) => $"{a}||{b}")}");
            }

            var mensaje = new Mensaje();

            var errores = string.Empty;

            errorList.ForEach(item => errores += item + Notificacion.TokenSaltoLinea);
            mensaje.Texto = errores;
            mensaje.Tipo = Notificacion.Error;

            return mensaje;
        }
    }
}
