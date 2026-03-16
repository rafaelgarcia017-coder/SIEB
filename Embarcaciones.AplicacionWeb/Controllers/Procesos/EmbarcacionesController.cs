using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Embarcaciones;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Security.Claims;

namespace Embarcaciones.AplicacionWeb.Controllers.Procesos
{
    public class EmbarcacionesController : CustomController
    {
        private readonly IEmbarcacionService _embarcacionService;
        private readonly ICatalogoService _catalogoService;
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly IMunicipioService _municipioService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IUnidadMedidaService _unidadMedidaService;

        private int IdUsuario => Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public EmbarcacionesController(IEmbarcacionService embarcacionService, ICatalogoService catalogoService, ICatalogoValorService catalogoValorService, IMunicipioService municipioService, IDepartamentoService departamentoService, IUnidadMedidaService unidadMedidaService)
        {
            _embarcacionService = embarcacionService;
            _catalogoService = catalogoService;
            _catalogoValorService = catalogoValorService;
            _municipioService = municipioService;
            _departamentoService = departamentoService;
            _unidadMedidaService = unidadMedidaService;
        }
        private async Task<int> ObtenerIdCatalogo(string codigoInterno)
        {
            var id = await _catalogoService.ObtenerIdCatalogo(codigoInterno);
            return id;
        }
        public IActionResult Administrar()
        {
            var model = new AdministrarEmbarcacionesVM
            {
                ListaEmbarcaciones = new List<EmbarcacionItem>()
            };

            return View("Administrar", model);
        }
        public async Task<IActionResult> NuevaEmbarcacion()
        {
            var model = await LlenarModelo(new EmbarcacionesVM());
            return View("NuevaEmbarcacion", model);
        }
        public async Task<IActionResult> GuardarEmbarcacion(EmbarcacionesVM model)
        {
            try
            {
                bool result = false;
                if (model.Accion == AccionesController.Nuevo)
                {
                    var embarcacion = MapearEmbarcacion(model);
                    result = await _embarcacionService.Agregar(embarcacion);
                }
                else
                {
                    var embarcacion = MapearEmbarcacion(model);
                    result = await _embarcacionService.Actualizar(embarcacion);
                }
                if (result)
                {
                  
                    AddExito(model.Accion == AccionesController.Nuevo
                     ? "Municipio registrado satisfactoriamente."
                     : "Municipio actualizado satisfactoriamente.");
                    return RedirectToAction("Administrar");
                }
                else
                {
                    AddError("Ha ocurrido un error. Contacte al Administrador.");
                    return View("NuevaEmbarcacion", LlenarModelo(model));
                }
                  
            }
            catch (Exception ex)
            {
                AddError("Ha ocurrido un error. Contacte al Administrador.");
                return View("NuevaEmbarcacion", LlenarModelo(model));
            }


        }

        private Embarcacion MapearEmbarcacion(EmbarcacionesVM model)
        {
            var construccion = new EmbarcacionConstruccion
            {
                Puntal = model.Construccion.Puntal,
                Trb = model.Construccion.TRB,
                Calado = model.Construccion.Calado,
                Trn = model.Construccion.TRN,
                Eslora = model.Construccion.Eslora,
                Manga = model.Construccion.Manga,
                SerieMotor = model.Construccion.Serie,
                NumeroTripulantes = model.Construccion.NumeroTripulantes,
                NumeroPasajeros = model.Construccion.NumeroPasajeros,
                AnioConstruccion = model.Construccion.AnioConstruccion,
                Potencia = model.Construccion.Potencia,
                MediosCx = model.Construccion.MedioCx,
                TipoFechaInfracciones = model.Construccion.TipoFechasInfracciones,
                NumeroConstruccion = model.Construccion.NumeroConstruccion,
                ModeloMotor = model.Construccion.Modelo,
                Frecuencia = model.Construccion.Frecuencia,
                CapacidadCarga = model.Construccion.CapacidadCarga,
                Indicativo = model.Construccion.Indicativo,
                IdUsuarioCreacion = IdUsuario,
                IdMaterial = model.Construccion.Material,
                IdPropulsion = model.Construccion.Propulsion,
                IdTipoComunicacion = model.Construccion.TipoComunicacion,
                IdMarcaMotor = model.Construccion.Marca,
                IdColorSuperestructura = model.Construccion.ColorSuperest,
                IdColorObraMuerta = model.Construccion.ColorM,
                IdColorObraViva = model.Construccion.ColorV,
                IdSistemaNavegacion = model.Construccion.SistemaNavegacion,
                IdUnidadMedidaPuntal = model.Construccion.UndMedPuntal,
                IdUnidadMedidaTrb = model.Construccion.UndMedTRB,
                IdUnidadMedidaTrn = model.Construccion.UndMedTRN,
                IdUnidadMedidaCalado = model.Construccion.UndMedCalado,
                IdUnidadMedidaEslora = model.Construccion.UndMedEslora,
                IdUnidadMedidaManga = model.Construccion.UndMedManga
            };

            var propietario = new EmbarcacionPropietario
            {
                NombrePropietario = model.Propietario.NombreCompleto,
                IdTipoIdentificacion = model.Propietario.TipoIdentificacion,
                Identificacion = model.Propietario.Identificacion,
                IdNacionalidad = model.Propietario.Nacionalidad,
                IdMunicipio = model.Propietario.Municipio,
                IdDepartamento = model.Propietario.Departamento,
                Domicilio = model.Propietario.Domicilio,
                Telefono = model.Propietario.Telefono,
                EmpresaPropietaria = model.Propietario.EmpresaPropietario,
                TelefonoEmpresaPropietaria = model.Propietario.TelefonoContacto,
                RutaImagenPropietario = model.Propietario.UrlImagen,
                LicenciaNavegacion = model.Propietario.LicenciaNavegacion,
                NumeroCarnetMarinero = model.Propietario.NumeroCarnetMarinero,
                NombreContacto = model.Propietario.NombreContacto,
                IdUsuarioCreacion = IdUsuario
            };

            return new Embarcacion
            {
                IdTipoEmbarcacion = model.Embarcacion.TipoEmbarcacion,
                IdPuertoRegistroAnterior = model.Embarcacion.PuertoRegistroAnterior,
                IdPuertoRegistroActual = model.Embarcacion.PuertoRegistroActual,
                IdBanderaRegistroActual = model.Embarcacion.BanderaActual,
                IdBanderaRegistroAnterior = model.Embarcacion.BanderaAnterior,
                IdActividad = model.Embarcacion.Actividad,
                IdZonaNavegacion = model.Embarcacion.ZonaNavegacion,
                NombreActual = model.Embarcacion.NombreActual,
                PropietarioAnterior = model.Embarcacion.PropietarioAnterior,
                FechaAbanderada = model.Embarcacion?.FechaAbanderamiento ?? null,
                FechaInscripcion = model.Embarcacion.FechaInscripcion,
                PermisoNavegacion = model.Embarcacion.PermisoNavegacion,
                LicenciaPesca = model.Embarcacion.LicenciaPesca,
                Distrito = model.Embarcacion.Distrito,
                MatriculaActual = model.Embarcacion.MatriculaActual,
                NombreAnterior = model.Embarcacion.NombreAnterior,
                IndicativoLlamada = model.Embarcacion.IndicativoLLamada,
                FechaExpiracion = model.Embarcacion.FechaExpiracion,
                LicenciaEspecialPesca = model.Embarcacion.LicenciaEspecialPesca,
                CapPce = model.Embarcacion.Cap_Pce,
                MatriculaAnterior = model.Embarcacion.MatriculaAnterior,
                NumeroOmi = model.Embarcacion.NumeroOmi,
                EstaActivo = true,
                EsHistorico = false,
                IdUsuarioCreacion = IdUsuario,
                FechaCreacion = DateTime.Now,
                EmbarcacionConstruccionNavigation = construccion,
                EmbarcacionPropietarioNavigation = propietario
            };
        }


        public async Task<EmbarcacionesVM> LlenarModelo(EmbarcacionesVM viewModel)
        {
            viewModel ??= new EmbarcacionesVM();
            int idTipoIdentificacion = await ObtenerIdCatalogo("TIDF");
            var tipoIdentificaciones = _catalogoValorService.ObtenerTodos(idTipoIdentificacion).ToList();
            viewModel.Propietario.ListaTipoIdentificacion = tipoIdentificaciones.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            var departamentos = _departamentoService.ObtenerTodos();
            viewModel.Propietario.ListaDepartamentos = departamentos.Select(x => new SelectListItem
            {
                Value = x.IdDepartamento.ToString(),
                Text = x.Departamento1
            }).ToList();

            int idBandera = await ObtenerIdCatalogo("BAND");
            var banderas = _catalogoValorService.ObtenerTodos(idBandera).ToList();
            viewModel.Embarcacion.ListaBanderaActual = banderas.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();
            viewModel.Embarcacion.ListaBanderaAnterior = banderas.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            int idActividad = await ObtenerIdCatalogo("ACTV");
            var actividades = _catalogoValorService.ObtenerTodos(idActividad).ToList();
            viewModel.Embarcacion.ListaActividad = actividades.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            int idZonaNavegacion = await ObtenerIdCatalogo("ZNNV");
            var zonaNavegacion = _catalogoValorService.ObtenerTodos(idZonaNavegacion).ToList();
            viewModel.Embarcacion.ListaZonaNavegacion = zonaNavegacion.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            int idMaterial = await ObtenerIdCatalogo("MTRL");
            var materiales = _catalogoValorService.ObtenerTodos(idMaterial).ToList();
            viewModel.Construccion.ListaMaterial = materiales.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            int idTipoPropulsion = await ObtenerIdCatalogo("TPPR");
            var tipoPropulsiones = _catalogoValorService.ObtenerTodos(idTipoPropulsion).ToList();
            viewModel.Construccion.ListaPropulsion = tipoPropulsiones.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            int idColor = await ObtenerIdCatalogo("COLR");
            var colores = _catalogoValorService.ObtenerTodos(idColor).ToList();
            viewModel.Construccion.ListaColorM = colores.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();
            viewModel.Construccion.ListaColorSuperest = colores.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();
            viewModel.Construccion.ListaColorV = colores.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            int idMarca = await ObtenerIdCatalogo("MRCA");
            var marcas = _catalogoValorService.ObtenerTodos(idMarca).ToList();
            viewModel.Construccion.ListaMarca = marcas.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            int idSistemaNavegacion = await ObtenerIdCatalogo("STNV");
            var sistemasNavegacion = _catalogoValorService.ObtenerTodos(idSistemaNavegacion).ToList();
            viewModel.Construccion.ListaSistemaNavegacion = sistemasNavegacion.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            int idPuerto = await ObtenerIdCatalogo("PRTO");
            var puertos = _catalogoValorService.ObtenerTodos(idPuerto).ToList();
            viewModel.Embarcacion.ListaPuertoRegistroActual = puertos.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();
            viewModel.Embarcacion.ListaPuertoRegistroAnterior = puertos.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();
            viewModel.Construccion.ListaPuerto = puertos.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();


            int idNacionalidad = await ObtenerIdCatalogo("NACI");
            var nacionalidad = _catalogoValorService.ObtenerTodos(idNacionalidad).ToList();
            viewModel.Propietario.ListaNacionalidad = nacionalidad.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            int idTipoEmbarcacion = await ObtenerIdCatalogo("TPEM");
            var tipoEmbarcacion = _catalogoValorService.ObtenerTodos(idTipoEmbarcacion).ToList();
            viewModel.Embarcacion.ListaTipoEmbarcacion = tipoEmbarcacion.Select(x => new SelectListItem
            {
                Value = x.IdCatalogoValor.ToString(),
                Text = x.Nombre
            }).ToList();

            var unidadmedida = _unidadMedidaService.ObtenerTodos();

            viewModel.Construccion.ListaUndCalado = unidadmedida.Select(x => new SelectListItem
            {
                Value = x.IdUnidadMedida.ToString(),
                Text = x.Abreviatura
            }).ToList();
            viewModel.Construccion.ListaUndMedEslora = unidadmedida.Select(x => new SelectListItem
            {
                Value = x.IdUnidadMedida.ToString(),
                Text = x.Abreviatura
            }).ToList();
            viewModel.Construccion.ListaUndMedManga = unidadmedida.Select(x => new SelectListItem
            {
                Value = x.IdUnidadMedida.ToString(),
                Text = x.Abreviatura
            }).ToList();
            viewModel.Construccion.ListaUndMedPuntal = unidadmedida.Select(x => new SelectListItem
            {
                Value = x.IdUnidadMedida.ToString(),
                Text = x.Abreviatura
            }).ToList();
            viewModel.Construccion.ListaUndMedTRB = unidadmedida.Select(x => new SelectListItem
            {
                Value = x.IdUnidadMedida.ToString(),
                Text = x.Abreviatura
            }).ToList();
            viewModel.Construccion.ListaUndMedTRN = unidadmedida.Select(x => new SelectListItem
            {
                Value = x.IdUnidadMedida.ToString(),
                Text = x.Abreviatura
            }).ToList();

            return viewModel;
        }
    }
}
