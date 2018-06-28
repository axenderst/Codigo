

function GoToNuevoMapa() {
    top.location = "../Administracion/CrearNuevoMapa";
}

function GoToAdministracionIndex() {
    top.location = "../Administracion/Index";
}

function GoToEditarMapa(idMapa) {
    top.location = "../Administracion/EditarMapa?idMapa=" + idMapa;
}


//Mostrar y ocultar elementos
function MostrarVentana(nombreDiv) {
    $("#divBackground").css("display", "block");
    $("#" + nombreDiv).css("display", "block");
}

function OcultarVentana(nombreDiv) {
    $("#divBackground").css("display", "none");
    $("#" + nombreDiv).css("display", "none");
}


function spinner() {
    $('#load_screen').css('display', 'block');
}
function OcultarCargando() {
    $("#load_screen").css("display", "none");
}


// Alertas
function MostrarMensajeExito(message) {
    swal("", message, "success");

}

function MostrarMensajeInformativo(message) {
    swal("", message, "info");
}

function MostrarMensajePrecaucion(message) {
    swal("", message, "warning");
}

function MostrarMensajeError(message) {
    swal("", message, "error");
}

function MostrarMensajeConfirmacion(title, message, funcion, w, x, y, z, u, v) {
    swal({
        title: title,
        text: message,
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#32BA7C',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            funcion(w, x, y, z, u, v);
        }
    })
}
// Alertas