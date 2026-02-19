
//const body = document.querySelector("body");
//const darkLight = document.querySelector("#darkLight");
//const sidebar = document.querySelector(".sidebar");
//const submenuItems = document.querySelectorAll(".submenu_item");
//const sidebarOpen = document.querySelector("#sidebarOpen");
//const sidebarClose = document.querySelector(".collapse_sidebar");
//const sidebarExpand = document.querySelector(".expand_sidebar");

//// Toggle sidebar open/close
//sidebarOpen.addEventListener("click", () => sidebar.classList.toggle("close"));
//sidebarClose.addEventListener("click", () => sidebar.classList.add("close", "hoverable"));
//sidebarExpand.addEventListener("click", () => sidebar.classList.remove("close", "hoverable"));

//sidebar.addEventListener("mouseenter", () => {
//    if (sidebar.classList.contains("hoverable")) sidebar.classList.remove("close");
//});
//sidebar.addEventListener("mouseleave", () => {
//    if (sidebar.classList.contains("hoverable")) sidebar.classList.add("close");
//});

//// Dark/Light mode toggle
//darkLight.addEventListener("click", () => {
//    body.classList.toggle("dark");
//    if (body.classList.contains("dark")) darkLight.classList.replace("fa-sun", "fa-moon");
//    else darkLight.classList.replace("fa-moon", "fa-sun");
//});

//// Submenu toggle
//submenuItems.forEach((item, index) => {
//    item.addEventListener("click", () => {
//        item.classList.toggle("show_submenu");
//        submenuItems.forEach((item2, index2) => {
//            if (index !== index2) item2.classList.remove("show_submenu");
//        });
//    });
//});

//// Sidebar responsive
//if (window.innerWidth < 768) sidebar.classList.add("close");
//else sidebar.classList.remove("close");

var TipoNotificaciones = {
    Info: 'info'
    , Error: 'error'
    , Exito: 'success'
    , Advertencia: 'warn'
};
function Notificacion(mensaje, estilo) {

    let alertType;
    let iconType;

    switch (estilo) {
        case 'error':
            alertType = 'danger';
            iconType = 'thumbs-down';
            break;
        case 'info':
            alertType = 'info';
            iconType = 'info';
            break;
        case 'success':
            alertType = 'success';
            iconType = 'thumbs-up';
            break;
        case 'warn':
            alertType = 'warning';
            iconType = 'exclamation-triangle';
            break;
        default:
            alertType = 'warning';
            iconType = 'exclamation-triangle';
            break;

    }

    let html = `
    <div class="custom-alert alert-${alertType}">
        <span class="alert-icon">
            <i class="fas fa-${iconType}"></i>
        </span>
        <span class="alert-text">${mensaje}</span>
        <button type="button" class="close-btn" onclick="$(this).parent().fadeOut()">
            ×
        </button>
    </div>
    `;

    $("#notifications").append(html);

     setTimeout(function () {
         $("#notifications").empty();
     }, 4500);

}

function showMessages(error, info, exito, advertencia) {
    if (error) {
        error = error.replace(/@@/g, '<br/>');
        Notificacion(error, TipoNotificaciones.Error);
    }

    if (info) {
        info = info.replace(/@@/g, '<br/>');
        Notificacion(info, TipoNotificaciones.Info);
    }

    if (exito) {
        exito = exito.replace(/@@/g, '<br/>');
        Notificacion(exito, TipoNotificaciones.Exito);
    }

    if (advertencia) {
        advertencia = advertencia.replace(/@@/g, '<br/>');
        Notificacion(advertencia, TipoNotificaciones.Advertencia);
    }

}

function BlockUI(mensajeBloqueo) {
    if ($('.blockUI.blockOverlay').length > 0) {
        UnBlockUI();
    }
    mensajeBloqueo = mensajeBloqueo ? mensajeBloqueo : '';
    $.blockUI({
        message: `<label>${mensajeBloqueo}</label><i class="la la-refresh spinner"></i>`,
        overlayCSS: {
            backgroundColor: '#FFF',
            opacity: 0.8,
            cursor: 'wait'
        },
        css: {
            color: '#333',
            border: 0,
            padding: 0,
            backgroundColor: 'transparent'
        }
    });
}

function UnBlockUI() {
    $.unblockUI();
}

function BlockUIControlEspecifico(idControl, mensajeBloqueo) {
    if (!idControl) return;

    mensajeBloqueo = mensajeBloqueo ? mensajeBloqueo : '';
    $(idControl).block({
        message: `<label>${mensajeBloqueo}</label><i class="la la-refresh spinner"></i>`,
        overlayCSS: {
            backgroundColor: '#FFF',
            opacity: 0.8,
            cursor: 'wait'
        },
        css: {
            color: '#333',
            border: 0,
            padding: 0,
            backgroundColor: 'transparent'
        }
    });
}

function UnBlockUIControlEspecifico(idControl) {
    $(idControl).unblock();
}


function AplicarDatatable(
    tabla,
    fixedLeftColumns = 0,
    scrollY = false,
    buscable = true,
    ordenar = true
) {

    let language = {
        processing: "Procesando...",
        lengthMenu: "Mostrar _MENU_ registros",
        zeroRecords: "No se encontraron resultados",
        emptyTable: "No hay datos para presentar",
        info: "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
        infoEmpty: "Mostrando registros del 0 al 0 de un total de 0 registros",
        infoFiltered: "(filtrado de un total de _MAX_ registros)",
        search: "Buscar:",
        loadingRecords: "Cargando...",
        paginate: {
            first: "Primero",
            last: "Último",
            next: "Siguiente",
            previous: "Anterior"
        }
    };

    let opciones = {
        language: language,
        destroy: true,
        ordering: ordenar,
        searching: buscable,
        scrollX: true,
        fixedColumns: fixedLeftColumns > 0 ? {
            leftColumns: fixedLeftColumns
        } : false
    };

    if (scrollY) {
        opciones.scrollY = "400px";
        opciones.scrollCollapse = true;
        opciones.paging = false;
    }

    $(tabla).DataTable(opciones);
}

function DestroyDatatable(tabla, eliminarRegistros) {
    $(tabla).dataTable({
        "bDestroy": true
    }).fnDestroy();


    if (eliminarRegistros == null)
        eliminarRegistros = true;


    if (eliminarRegistros) {
        $(`${tabla} tbody`).empty();

    }

}
