$(document).ready(function () {
   
   // hideButton();
    $("#calendarioMes").kendoDatePicker({
        // defines the start view
        start: "year",

        // defines when the calendar should return date
        depth: "year",

        // display month and year in the input
        format: "MMMM yyyy",

        // specifies that DateInput is used for masking the input element
        dateInput: true,

        change: onChangeDate
        
    });

 
    $("#dialog").kendoDialog({
        width: "1200px",
        visible: false,
        title: "Detalles del catalogo temporal",
        closable: true,
        modal: false
    });
});




function tipoEjecucion(ismensual) {
    var mensual = document.getElementById("divCargaMensual");
    var manual = document.getElementById("divCargaManual");
    if (ismensual) {
        mensual.style.display = "block";
        manual.style.display = "none";
    } else {
        mensual.style.display = "none";
        manual.style.display = "block";
    }
}

function insertarCatalogo() {
    $.ajax({
        type: "POST",
        url: "../Extraccion/GuardarCatalogo",
        data: {},
        cache: false,
        success: function (result) {
            if (result.ProcesoExitoso == 1) {
                //Actualizamos los datos del grid
                onChangeDate();
                MostrarMensajeExito("La información se ha cargado correctamente a la tabla CATALOGO.");
            } else {
                MostrarMensajeError(result.Mensaje);
            }
        }

    });
}


function procesarArchivo() {
    //Validamos que se haya seleccionado una fecha
    var fecha = kendo.format("{00:" + 'dd/MM/yyyy' + "}", $("#calendarioMes").data("kendoDatePicker").value());
    if (fecha == "null") {
        MostrarMensajeError("Debe proporcionar una fecha de ejecución.");
        return false;
    }

    $("#gvDatosCargaManual").empty();
    //Obtenemos los datos del archivo para enviarlo al servidor
    var formData = new FormData();
    formData.append("FileUpload", document.getElementById('flArchivo').files[0]);
    formData.append("mesProceso", fecha);
    spinner();
    $.ajax({
        type: "POST",
        url: "../Extraccion/ProcesarArchivo",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        type: 'POST',
        success: function (result) {
            if (result.ProcesoExitoso == 1) {
                ArmaGridCatalogoManual(result.Lista);
                //Actualizamos los datos del grid
                onChangeDate();
                //Habilitamos el boton para cargar la informacion
                $('#btnInsertar').prop("disabled", false);

            } else if (result.ProcesoExitoso == 3) {
                MostrarMensajeConfirmacion(result.Mensaje, "¿Desea configurar un nuevo mapa para este archivo?", GoToNuevoMapa);
            } else if (result.ProcesoExitoso == 4) {
                $("#messageLog").html(result.Mensaje);
                MostrarVentana('ViewLog');
            }
            else {
                MostrarMensajeError(result.Mensaje);
            }
            OcultarCargando();


        }
    });
}


function procesarCarpeta() {
   
    if (fecha = kendo.format("{00:" + 'dd/MM/yyyy' + "}", $("#calendarioMes").data("kendoDatePicker").value()) == "null") {
        MostrarMensajeError("Debe proporcionar una fecha de ejecución.");
        return false;
    }
    spinner();

    //Obtenemos los datos del archivo para enviarlo al servidor
    $.ajax({
        type: "POST",
        url: "../Extraccion/ProcesarCarpeta",
        data: { "mesProceso": kendo.format("{00:" + 'dd/MM/yyyy' + "}", $("#calendarioMes").data("kendoDatePicker").value()) },

        cache: false,
        type: 'POST',
        success: function (result) {
            if (result.ProcesoExitoso == 1) {
                //Cargamos los datos procesados de la carpeta.
                ArmaGridCatalogoMensual(result.Lista);

                //Cargamos los datos de error.
                ArmaGridCatalogoError(result.Listado);
                $('#btnCargar').prop("disabled", false);
            } else {
                MostrarMensajeError(result.Mensaje);
            }
            OcultarCargando();
        }
    });
    
} 

function DetalleLog(llproceso, llmapa) {
    var strLog = "";
    strLog = "<a onclick=MostrarLog(" + llproceso + "," + llmapa + ")><img src='../Images/log.png' style='vertical-align: middle; width:35px;'/></a>";

    return strLog;
}

function DetalleMapa(llestatusMapa, llprocesos, llmapa) {
    var strTemplate = "";
    if (llestatusMapa == 1 || llestatusMapa == 2) {
        strTemplate = "<a  onclick=MostrarDetalleMapa(" + llprocesos + "," + llmapa + ")><img src='../Images/View-Details.png' style='vertical-align: middle; width:35px;'/></a>";
    }
    return strTemplate;
}
function MostrarLog(llproceso, llmapa) {
    

        $.ajax({
            type: "POST",
            dslogMapa: "dslog",
            cache: false,
            url: "../Extraccion/ConsultarLog",
            data:{"llproceso" : llproceso, "llmapa": llmapa},
            success: function (resultado) {

                $("#messageLog").html(resultado.Mensaje);
                MostrarVentana('ViewLog');

            }
       });
}

function ArmaGridCatalogoManual(Datos) {
    $("#gvDatosCargaManual").kendoGrid({
        dataSource: {
                data: Datos,
                schema: {
                    model: {
                        fields: {
                            IdCatalog: "IdCatalog",
                            Mayorista: "Mayorista",
                            Presentacion: "Presentacion",
                            Fecha: "Fecha",
                            Unidades: "Unidades",
                            Medico: "Medico",
                            Estado: "Estado",
                            Hospital: "Hospital",
                            Sucursal: "Sucursal",
                            Laboratorio: "Laboratorio",
                            Ciudad: "Ciudad",
                            Colonia: "Colonia",
                            Direccion: "Direccion",
                            CP: "CP",
                            Brick: "Brick",
                            Id_mapa: "Id_mapa",
                        }
                    }
                },
                pageSize: 10,
                cache: false
            },
        selectable: "row",
        scrollable: false,
        sortable: false,
        filterable: false,
        pageable: {
            refresh: false,
            pageSizes: true,
            input: true,
            numeric: false
        },
        columns: [
            {
                field: "IdCatalog",
                title: "IdCatalog",
                width: "10%",
                hidden: true
            },
            {
                field: "Mayorista",
                title: "Mayorista"
            },
            {
                field: "Presentacion",
                title: "Presentacion"
            },
            {
                field: "Fecha",
                title: "Fecha",
            },
            {
                field: "Unidades",
                title: "Unidades",
            },
            {
                field: "Medico",
                title: "Medico",
            },
            {
                field: "Estado",
                title: "Estado",
            },
            {
                field: "Hospital",
                title: "Hospital",
            },
            {
                field: "Sucursal",
                title: "Sucursal",
            },
            {
                field: "Laboratorio",
                title: "Laboratorio",
            },
            {
                field: "Ciudad",
                title: "Ciudad",
            },
            {
                field: "Colonia",
                title: "Colonia",
            },
            {
                field: "Direccion",
                title: "Direccion",
            },
            {
                field: "CP",
                title: "CP",
            },
            {
                field: "Brick",
                title: "Brick",
            }

        ]
    });
}

function ArmaGridCatalogoMensual(Datos) {
    $("#gvEstatusCarga").empty();
    $("#gvEstatusCarga").kendoGrid({
        dataSource: {
            data: Datos,
            schema: {
                model: {
                    fields: {
                        llprocesos: "llprocesos",
                        boprocesomanual: "boprocesomanual",
                        fcinicio: "fcinicio",
                        llmapa: "llmapa",
                        dsmapa: "dsmapa",
                        dsarchivo: "dsarchivo",
                        llestatusMapa: "llestatusMapa",
                    }
                }
            },
            pageSize: 10,
            cache: false
        },
        selectable: "row",
        scrollable: true,
        sortable: false,
        filterable: false,
        pageable: {
            refresh: false,
            pageSizes: true,
            input: true,
            numeric: false
        },
        columns: [
            {
                field: "llprocesos",
                title: "llprocesos",
                width: "10px",
                hidden: true
            },
            {
                title: "Inicio",
                field: "fcinicio"
            },
            {
                title: "llmapa",
                field: "llmapa",
                hidden: true
            },
            {
                title: "Mapa",
                field: "dsmapa",
            },
            {
                title: "Archivo",
                field: "dsarchivo",
            },
            //{
            //    title: "Log Mapa",
            //    template: '#= DetalleLog(llproceso, llmapa) #',
            //},
            {
                title: "Estatus",
                template: '#= ImagenTempleteStatus(llestatusMapa) #', 
                field: "llestatusMapa",
            },
            {
                title: "Detalle",
                template: '#= DetalleMapa(llestatusMapa, llproceso, llmapa) #',
            },
            {
                title: "Administración de Mapa",
                template: '#= LigaAdminMapa(dsmapa, llmapa) #',
                field: "urlMapa"
            }
        ]
    });

}

function ImagenTempleteStatus(llestatusMapa) {
    var a = "";
    var v = "";
    var r = "";
    if (llestatusMapa == 1) {
        a = "../Images/2d.png"
        return "<img src='" + (a) + "' title='El mapa se ha creado correctamente: falta cargar en catalogo' style='vertical-align: middle; width:35px;'/>";
    }else if (llestatusMapa == 3) {
        r = "../Images/3d.png";
        return "<img src='" + (r) + "' title='El mapa no existe en el sistema: proceda a crearlo o modifique'  style='vertical-align: middle; width:35px;'/>";
    }else if (llestatusMapa == 2) {
        v = "../Images/1d.png";
        return "<img src='" + (v) + "' title='Mapa procesado correctamente, se encuentra registrado en catalogo' style='vertical-align: middle; width:35px;'/>";
    } else if (llestatusMapa == 4) {
        r = "../Images/3d.png";
        return "<img src='" + (r) + "' title='Se encontro un error al momento de cargar el archivo: Modifique el mapa' style='vertical-align: middle; width:35px;'/>";
    }
}

function LigaAdminMapa(dsmapa, llmapa) {
    respuesta = '';
    if (dsmapa) {
        respuesta = "<a onclick=GoToEditarMapa(" + llmapa + ")>Modificar Mapa</a>";
    } else {
        respuesta = '<a onclick="GoToNuevoMapa()">Crear Nuevo Mapa</a>';
    } 
    return respuesta;
}

function DetalleMapa(llestatusMapa, llprocesos, llmapa){
    var strTemplate = "";
    if (llestatusMapa == 1 || llestatusMapa == 2) {
        strTemplate = "<a  onclick=MostrarDetalleMapa(" + llprocesos + "," + llmapa + ","+ llestatusMapa + ")><img src='../Images/View-Details.png' style='vertical-align: middle; width:35px;'/></a>";
    }
    return strTemplate;
}


function MostrarDetalleMapa(llprocesos, llmapa, llestatus) {
    $("#gvDatosVentana").empty();
    MostrarVentana('detalleCatalogo');
    $.ajax({
        type: "POST",
        data: { "IdMapa": llmapa, "llproceso": llprocesos, "llestatus": llestatus },
        cache: false,
        url: "../Extraccion/DetalleProcesoMapa",
        success: function (resultado) {
            if (resultado.ProcesoExitoso == 1) {
                gridVentanaEmergente(resultado.Lista);
            } else {
                MostrarMensajeError(resultado.Mensaje);
            }
        }
    });
}


function gridVentanaEmergente(muestra) {
    $("#gvDatosVentana").empty();
    $("#gvDatosVentana").kendoGrid({
        dataSource: {
            data: muestra,
            schema: {
                model: {
                    fields: {
                        IdCatalog: "IdCatalog",
                        Mayorista: "Mayorista",
                        Presentacion: "Presentacion",
                        Fecha: "Fecha",
                        Unidades: "Unidades",
                        Medico: "Medico",
                        Estado: "Estado",
                        Hospital: "Hospital",
                        Sucursal: "Sucursal",
                        Laboratorio: "Laboratorio",
                        Ciudad: "Ciudad",
                        Colonia: "Colonia",
                        Direccion: "Direccion",
                        CP: "CP",
                        Brick: "Brick",
                        Id_mapa: "Id_mapa",
                    }
                }
            },
            pageSize: 5,
            cache: false
        },

        selectable: "row",
        scrollable: true,
        sortable: true,
        pageable: {
            refresh: false,
            pageSizes: false,
            input: true,
            numeric: false
        },
        columns: [
            {
                field: "IdCatalog",
                title: "IdCatalog",
                width: "10%",
                hidden: true
            },
            {
                field: "Mayorista",
                title: "Mayorista"
            },
            {
                field: "Presentacion",
                title: "Presentacion"
            },
            {
                field: "Fecha",
                title: "Fecha",
            },
            {
                field: "Unidades",
                title: "Unidades",
            },
            {
                field: "Medico",
                title: "Medico",
            },
            {
                field: "Estado",
                title: "Estado",
            },
            {
                field: "Hospital",
                title: "Hospital",
            },
            {
                field: "Sucursal",
                title: "Sucursal",
            },
            {
                field: "Laboratorio",
                title: "Laboratorio",
            },
            {
                field: "Ciudad",
                title: "Ciudad",
            },
            {
                field: "Colonia",
                title: "Colonia",
            },
            {
                field: "Direccion",
                title: "Direccion",
            },
            {
                field: "CP",
                title: "CP",
            },
            {
                field: "Brick",
                title: "Brick",
            }

        ]
    });
}


function cargarInformacionMensual() {
    spinner();
    $.ajax({
        type: "POST",
        url: "../Extraccion/GuardarCatalogoMensual",
        data: {},
        cache: false,
        async: false,
        success: function (result) {
            if (result.ProcesoExitoso == 1) {
                //Actualizamos los datos del grid
                onChangeDate();
                MostrarMensajeExito("La información se ha cargado correctamente a la tabla CATALOGO.");
            } else {
                MostrarMensajeError(result.Mensaje);
            }
        }

    });
    OcultarCargando();
}

function onChangeDate() {
    //alert(kendo.format("{0:" + 'dd/MM/yyyy' + "}", $("#calendarioMes").data("kendoDatePicker").value()));
    $('#btnCargar').prop("disabled", true);
    //Revisamos si existen registros ejecutados en esta fecha
    spinner();
    //Obtenemos los datos del archivo para enviarlo al servidor
    $.ajax({
        type: "POST",
        url: "../Extraccion/ConsultarProceso",
        data: { "mesProceso": kendo.format("{00:" + 'dd/MM/yyyy' + "}", $("#calendarioMes").data("kendoDatePicker").value()) },

        cache: false,
        success: function (result) {
            if (result.ProcesoExitoso == 1) {
                /* Cargamos los datos del proceso mensual */
                ArmaGridCatalogoMensual(result.Lista);

                /* Cargamos los datos que se tienen que cargar manualmente */
                ArmaGridCatalogoError(result.Listado);
                $('#btnCargar').prop("disabled", false);
            } else if (result.ProcesoExitoso == 0) {
                /*Mostramos Mensaje de error */
                MostrarMensajeError(result.Mensaje);
            } else if (result.ProcesoExitoso == 2) {
                /*Mostramos Mensaje de error */
                $("#gvEstatusCargaError").empty();
                $("#gvEstatusCarga").empty();
            }
            OcultarCargando();
        },
        error: function (xhr, ajaxOptions, thrownError){
            OcultarCargando();
        }
    });
}



function ArmaGridCatalogoError(Datos) {
    $("#gvEstatusCargaError").empty();
    $("#gvEstatusCargaError").kendoGrid({
        dataSource: {
            data: Datos,
            schema: {
                model: {
                    fields: {
                        llprocesos: "llprocesos",
                        boprocesomanual: "boprocesomanual",
                        fcinicio: "fcinicio",
                        llmapa: "llmapa",
                        dsmapa: "dsmapa",
                        dsarchivo: "dsarchivo",
                        dslogMapa: "dslogMapa",
                        llestatusMapa: "llestatusMapa",
                        urlMapa: "urlMapa",
                    }
                }
            },
            pageSize: 10,
            cache: false
        },
        selectable: "row",
        scrollable: true,
        sortable: false,
        filterable: false,
        pageable: {
            refresh: false,
            pageSizes: true,
            input: true,
            numeric: false
        },
        columns: [
            {
                field: "llprocesos",
                title: "llprocesos",
                width: "10px",
                hidden: true
            },
            {
                title: "Inicio",
                field: "fcinicio"
            },
            {
                title: "llmapa",
                field: "llmapa",
                hidden: true
            },
            {
                title: "Mapa",
                field: "dsmapa",
            },
            {
                title: "Archivo",
                field: "dsarchivo",
                
            },

            {
                title: "Estatus",
                template: '#= ImagenTempleteStatus(llestatusMapa) #',
                field: "llestatusMapa",
            },

            {
                title: "Administración de Mapa",           
                template: '#= LigaAdminMapa(dsmapa, llmapa) #',
                field: "urlMapa"
            }

        ]
    });

}