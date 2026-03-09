using Embarcaciones.AplicacionWeb.Models.ViewModels.Procesos.Embarcaciones;
using Embarcaciones.BLL.Service;
using Embarcaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace Embarcaciones.AplicacionWeb.Controllers.Procesos
{
    public class EmbarcacionesController : CustomController
    {
        private readonly ICatalogoService _catalogoService;
        private readonly ICatalogoValorService _catalogoValorService;
        private readonly IMunicipioService _municipioService;
        private readonly IDepartamentoService _departamentoService;
        private readonly IUnidadMedidaService _unidadMedidaService;

        public EmbarcacionesController(ICatalogoService catalogoService, ICatalogoValorService catalogoValorService, IMunicipioService municipioService, IDepartamentoService departamentoService, IUnidadMedidaService unidadMedidaService)
        {
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
