using Embarcaciones.AplicacionWeb.Models.Utils;
using Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Embarcaciones;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICatalogoFormularioService _catalogoFormularioService;

        private int IdUsuario => Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public EmbarcacionesController(IEmbarcacionService embarcacionService, ICatalogoService catalogoService, ICatalogoValorService catalogoValorService, IMunicipioService municipioService, IDepartamentoService departamentoService, IUnidadMedidaService unidadMedidaService, ICatalogoFormularioService catalogoFormularioService)
        {
            _embarcacionService = embarcacionService;
            _catalogoService = catalogoService;
            _catalogoValorService = catalogoValorService;
            _municipioService = municipioService;
            _departamentoService = departamentoService;
            _unidadMedidaService = unidadMedidaService;
            _catalogoFormularioService = catalogoFormularioService;
        }
        private async Task<int> ObtenerIdCatalogo(string codigoInterno)
        {
            var id = await _catalogoService.ObtenerIdCatalogo(codigoInterno);
            return id;
        }

        public async Task<string?> GuardarImagen(IFormFile archivo, string carpetaDestino = "Imagenes")
        {
            if (archivo == null || archivo.Length == 0)
                return null;

            // Validar tipo de archivo (solo imágenes)
            var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(archivo.FileName).ToLower();

            if (!extensionesPermitidas.Contains(extension))
                throw new Exception("Formato de imagen no permitido.");

            // Validar tamaño (ejemplo: 5MB)
            if (archivo.Length > 5 * 1024 * 1024)
                throw new Exception("La imagen excede el tamaño permitido (5MB).");

            // Ruta base
            var rutaRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            // Carpeta dinámica
            var carpeta = Path.Combine(rutaRoot, carpetaDestino);

            // Crear carpeta si no existe
            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            // Nombre único
            var nombreArchivo = $"{Guid.NewGuid()}{extension}";

            // Ruta completa
            var rutaCompleta = Path.Combine(carpeta, nombreArchivo);

            // Guardar archivo
            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // Retornar ruta relativa
            return $"/{carpetaDestino}/{nombreArchivo}";
        }


        private async Task<string> ManejarImagenAsync(IFormFile imagenFile, string urlActual, bool eliminar)
        {
            // 1. Si hay nueva imagen
            if (imagenFile != null)
            {
                if (!string.IsNullOrEmpty(urlActual))
                {
                    var rutaAnterior = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        urlActual.TrimStart('/')
                    );

                    if (System.IO.File.Exists(rutaAnterior))
                        System.IO.File.Delete(rutaAnterior);
                }

                return await GuardarImagen(imagenFile);
            }

            // 2. Si se solicita eliminar sin subir nueva
            if (eliminar && imagenFile == null)
            {
                if (!string.IsNullOrEmpty(urlActual))
                {
                    var rutaAnterior = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        urlActual.TrimStart('/')
                    );

                    if (System.IO.File.Exists(rutaAnterior))
                        System.IO.File.Delete(rutaAnterior);
                }

                return null;
            }

            // 3. Mantener imagen actual
            return urlActual;
        }

        public async Task<IActionResult> Administrar()
        {
            var embarcacionesList = await _embarcacionService.ObtenerTodos();

            var model = new AdministrarEmbarcacionesVM
            {
                ListaEmbarcaciones = embarcacionesList.Select(s => new EmbarcacionItem
                {
                    IdEmbarcacion = s.IdEmbarcacion,
                    Propietario = s.Propietario,
                    Identificacion = s.Identificacion,
                    Departamento = s.Departamento,
                    Municipio = s.Municipio,
                    Licencia = s.Licencia,
                    NombreActual = s.NombreActual,
                    Matricula = s.Matricula,
                    OMI = s.OMI,
                    PermisoNavegacion = s.PermisoNavegacion,
                    FechaExpiracion = s.FechaExpiracion,
                    UsuarioCreacion = s.UsuarioCreacion,
                    FechaCreacion = s.FechaCreacion.ToString("dd/MM/yyyy"),
                    UsuarioModificacion = s.UsuarioModificacion,
                    FechaModificacion = s.FechaModificacion?.ToString("dd/MM/yyyy") ?? string.Empty,
                }).ToList()
            };

            return View("Administrar", model);
        }
        public async Task<IActionResult> NuevaEmbarcacion()
        {
            var model = await LlenarModelo(new EmbarcacionesVM());
            return View("NuevaEmbarcacion", model);
        }

        public async Task<IActionResult> EditarEmbarcacion(int id)
        {
            var embarcacion = await _embarcacionService.Obtener(id);
            var model = MapearAViewModel(embarcacion);
            model.Accion = AccionesController.Editar;
            model = await LlenarModelo(model);
            return View("NuevaEmbarcacion", model);
        }
        public async Task<IActionResult> GuardarEmbarcacion(EmbarcacionesVM model)
        {
            try
            {
                bool result = false;

                model.Propietario.UrlImagen = await ManejarImagenAsync(
                    model.Propietario.ImagenFile,
                    model.Propietario.UrlImagen,
                    model.Propietario.EliminarImagenPropietario
                );

                model.Embarcacion.UrlImagen = await ManejarImagenAsync(
                    model.Embarcacion.ImagenFile,
                    model.Embarcacion.UrlImagen,
                    model.Embarcacion.EliminarImagenEmbarcacion
                );

                var embarcacion = MapearEmbarcacion(model);

                if (model.Accion == AccionesController.Nuevo)
                {
                    result = await _embarcacionService.Agregar(embarcacion);
                }
                else
                {
                    embarcacion.IdUsuarioModificacion = IdUsuario;
                    embarcacion.FechaModificacion = DateTime.Now;
                    result = await _embarcacionService.Actualizar(embarcacion);
                }

                if (result)
                {
                    AddExito(model.Accion == AccionesController.Nuevo
                     ? "Embarcacion registrada satisfactoriamente."
                     : "Embarcacion actualizada satisfactoriamente.");
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
            return new Embarcacion
            {
                IdEmbarcacion = model.Embarcacion.IdEmbarcacion,
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
                RutaImagenEmbarcacion = model.Embarcacion.UrlImagen,
                LicenciaNavegacion = model.Propietario.LicenciaNavegacion,
                NumeroCarnetMarinero = model.Propietario.NumeroCarnetMarinero,
                NombreContacto = model.Propietario.NombreContacto,
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
                IdUnidadMedidaManga = model.Construccion.UndMedManga,
                IdUsuarioCreacion = IdUsuario,
                EstaActivo = true,
                EsHistorico = false,
                FechaCreacion = DateTime.Now,

            };
        }

        private EmbarcacionesVM MapearAViewModel(Embarcacion entity)
        {
            return new EmbarcacionesVM
            {
                Propietario = new PropietarioEmbarcacionesVM
                {
                    NombreCompleto = entity.NombrePropietario,
                    TipoIdentificacion = entity.IdTipoIdentificacion,
                    Identificacion = entity.Identificacion,
                    Nacionalidad = entity.IdNacionalidad,
                    Municipio = entity.IdMunicipio,
                    Departamento = entity.IdDepartamento,
                    Domicilio = entity.Domicilio,
                    Telefono = entity.Telefono,
                    EmpresaPropietario = entity.EmpresaPropietaria,
                    TelefonoContacto = entity.TelefonoEmpresaPropietaria,
                    UrlImagen = entity.RutaImagenPropietario,
                    LicenciaNavegacion = entity.LicenciaNavegacion,
                    NumeroCarnetMarinero = entity.NumeroCarnetMarinero,
                    NombreContacto = entity.NombreContacto
                },

                Embarcacion = new DatosEmbarcacionesVM
                {
                    IdEmbarcacion = entity.IdEmbarcacion,
                    TipoEmbarcacion = entity.IdTipoEmbarcacion,
                    PuertoRegistroAnterior = entity.IdPuertoRegistroAnterior,
                    PuertoRegistroActual = entity.IdPuertoRegistroActual,
                    BanderaActual = entity.IdBanderaRegistroActual,
                    BanderaAnterior = entity.IdBanderaRegistroAnterior,
                    Actividad = entity.IdActividad,
                    ZonaNavegacion = entity.IdZonaNavegacion,
                    UrlImagen = entity.RutaImagenEmbarcacion,
                    NombreActual = entity.NombreActual,
                    PropietarioAnterior = entity.PropietarioAnterior,
                    FechaAbanderamiento = entity.FechaAbanderada,
                    FechaInscripcion = entity.FechaInscripcion,
                    PermisoNavegacion = entity.PermisoNavegacion,
                    LicenciaPesca = entity.LicenciaPesca,
                    Distrito = entity.Distrito,
                    MatriculaActual = entity.MatriculaActual,
                    NombreAnterior = entity.NombreAnterior,
                    IndicativoLLamada = entity.IndicativoLlamada,
                    FechaExpiracion = entity.FechaExpiracion,
                    LicenciaEspecialPesca = entity.LicenciaEspecialPesca,
                    Cap_Pce = entity.CapPce,
                    MatriculaAnterior = entity.MatriculaAnterior,
                    NumeroOmi = entity.NumeroOmi
                },

                Construccion = new ConstruccionEmbarcacionesVM
                {
                    Puntal = entity.Puntal,
                    TRB = entity.Trb,
                    Calado = entity.Calado,
                    TRN = entity.Trn,
                    Eslora = entity.Eslora,
                    Manga = entity.Manga,
                    Serie = entity.SerieMotor,
                    NumeroTripulantes = entity.NumeroTripulantes,
                    NumeroPasajeros = entity.NumeroPasajeros,
                    AnioConstruccion = entity.AnioConstruccion,
                    Potencia = entity.Potencia,
                    MedioCx = entity.MediosCx,
                    TipoFechasInfracciones = entity.TipoFechaInfracciones,
                    NumeroConstruccion = entity.NumeroConstruccion,
                    Modelo = entity.ModeloMotor,
                    Frecuencia = entity.Frecuencia,
                    CapacidadCarga = entity.CapacidadCarga,
                    Indicativo = entity.Indicativo,
                    Material = entity.IdMaterial,
                    Propulsion = entity.IdPropulsion,
                    TipoComunicacion = entity.IdTipoComunicacion,
                    Marca = entity.IdMarcaMotor,
                    ColorSuperest = entity.IdColorSuperestructura,
                    ColorM = entity.IdColorObraMuerta,
                    ColorV = entity.IdColorObraViva,
                    SistemaNavegacion = entity.IdSistemaNavegacion,
                    UndMedPuntal = entity.IdUnidadMedidaPuntal,
                    UndMedTRB = entity.IdUnidadMedidaTrb,
                    UndMedTRN = entity.IdUnidadMedidaTrn,
                    UndMedCalado = entity.IdUnidadMedidaCalado,
                    UndMedEslora = entity.IdUnidadMedidaEslora,
                    UndMedManga = entity.IdUnidadMedidaManga
                }
            };
        }

        [HttpGet]
        public async Task<ActionResult> ObtenerMunicipioPorDepartamento(int id)
        {
            var resultado = _catalogoFormularioService.ObtenerMunicipioPorDepartamento(id);
            var lista = await resultado.Select(s => new { id = s.IdMunicipio, value = s.Municipio1 }).ToListAsync();

            return Json(lista);
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

            if (viewModel.Accion == AccionesController.Editar && viewModel.Propietario.Departamento > 0)
            {
                var resultado = _catalogoFormularioService.ObtenerMunicipioPorDepartamento(viewModel.Propietario.Departamento);
                var lista = resultado.Select(x => new SelectListItem
                {
                    Value = x.IdMunicipio.ToString(),
                    Text = x.Municipio1
                }).ToList();

                viewModel.Propietario.ListaMunicipio = lista;

            }

            return viewModel;
        }
    }
}
