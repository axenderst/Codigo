$(document).ready(function () {
    $("#aviso").kendoDialog({
        width: "500px",
        visible: false,
        title: "Error encontrado:",
        closable: true,
        modal: false
    });
});

function GoToNuevoMapa() {
    top.location = "../Administracion/CrearNuevoMapa";
}
function GoToBuscar() {
    ObtenerRegistrosSeguimiento();
}

function GoToAdministracionIndex() {
    top.location = "../Administracion/Index";
}

function archivoCargado() {
    //Obtenemos los datos del archivo para enviarlo al servidor
    spinner();
    // Limpiamos y grid de excel
    $("#ddlnoHoja").empty();
    $("#datosExcel").empty();
    $("#claveMapa").val('');

    var formData = new FormData();
    formData.append("FileUpload", document.getElementById('flArchivo').files[0]);
    $.ajax({
        type: "POST",
        url: "../Administracion/AnalisisPlantillaExcel",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        type: 'POST',
        success: function (result) {
            if (result.ProcesoExitoso == 1) {
                var ddlHojas = $("#ddlnoHoja");
                ddlHojas.html('');
                $.each(result.Lista, function (id, option) {
                    ddlHojas.append($('<option></option>').val(option.noHoja).html(option.Nombre));
                });

                $("#claveMapa").val(result.Cadena);
            } else {
                MostrarMensajeError(result.Cadena);
            }
            OcultarCargando();
        }
    });
}

function ObtenerRegistrosSeguimiento() {
 
        $.ajax({
            type: "POST",
            url: "../Mapa/ListarMapas",
            data: {
                 Mapa: 1,
                ClaveMapa: $("#nomMapa").val(),
                Hoja: 1,
                Encabezado:1,
                FechaCreacion: $("#fechaCreacion").val(),
                FechaModif: $("#fechaUltimaModif").val()

            },
            cache: false,
            success: function (result) {
                if (result.ProcesoExitoso == 1) {
                    ArmaGridMapa(result.Lista);
                }
                else {
                    MostrarMensajeError(result.Mensaje);
                }
            }
        });
    }

    function ArmaGridMapa(Datos) {
        $("#gvMapas").kendoGrid({
            dataSource: {
                data: Datos,
                schema: {
                    model: {
                        fields: {
                            IdMapa: "IdMapa",
                            Sufijo: "Sufijo",
                            Hoja: "Hoja",
                            FilaEncabezado: "FilaEncabezado",
                            FechaCreacion: "FechaCreacion",
                            Fecha_UltimaModif: "Fecha_UltimaModif"
                        }
                    }
                },
                pageSize: 10
            },
            selectable: "row",
            scrollable: false,
            sortable: false,
            filterable: true,
            pageable: {
                refresh: false,
                pageSizes: true,
                input: true,
                numeric: false
            },
            columns: [
                {
                    field: "mapaId",
                    title: "IdMapa",
                    width: "10%",
                    hidden: true
                },
                {
                    field: "ClaveMapa",
                    title: "ClaveMapa",
                    width: "25%"
                },
                {
                    field: "hoja",
                    title: "Hoja",
                    width: "10%"
                },
                {
                    field: "filaEncabezado",
                    title: "FilaEncabezado",
                    width: "10%"
                },
                {
                    field: "fechaCreacion",
                    title: "FechaCreacion",
                    width: "15%"
                },
                {
                    field: "fechaUltimaModif",
                    title: "Fecha_UltimaModif",
                    width: "15%"
                }
            ]
        });

    }


function SeleccionHoja() {
    var x = document.getElementById("ddlnoHoja").value;
    var y = document.getElementById("nofila").value;


    if (y <= 0) {
        MostrarMensajeError("Debe seleccionar un fila apartir de 1");
    } else {
        spinner();
        $.ajax({
            type: "POST",
            url: "../Administracion/CargarDatosExcel",
            data: { "noHoja": x, "noFila": y },
            cache: false,
            success: function (result) {
                if (result.ProcesoExitoso == 1) {
                    var divCelldas = $("#datosExcel");
                    divCelldas.html('');
                    var codehtml = '<table class="ExcelTableXP" border="1"><tbody>[campos_nuevos]';
                    var fila = 0;
                    var filaanterior = 0;
                    var primeraFila = 0;
                    var headers = ' align="left" valign="bottom" class="heading" ';
                    var opciones = '';
                    var valorColumna = 0;
                    $.each(result.Lista, function (id, option) {
                        fila = option.fila;
                        //Cargamos los combos para elegir la columna.
                        if (fila != filaanterior) {
                            if (primeraFila == 1) {
                                codehtml = codehtml + '</tr>';
                            }
                            codehtml = codehtml + '<tr>';
                            if (fila == y) {
                                opciones = opciones + '<th class="heading"></th>';
                                codehtml = codehtml + '<td align="left" valign="bottom" class="heading">' + (fila) + '</td>';
                            } else {
                                codehtml = codehtml + '<td align="left" valign="bottom" class="heading">' + (fila) + '</td>';
                            }


                            primeraFila = 1;
                        }
                        if (fila == y) {
                            opciones = opciones + '<th  class="heading"><select style="width:110px;" class="form-control" name="ddlnoHoja' + valorColumna + '" id="ddlnoHoja' + valorColumna + '"><option value="0">Seleccione</option><option value="1">Mayorista</option><option value="2">Presentacion</option><option value="3">Fecha</option><option value="4">Unidades</option><option value="5">Medico</option><option value="6">Estado</option><option value="7">Hospital</option><option value="8">Sucursal</option><option value="9">Laboratorio</option><option value="10">Ciudad</option><option value="11">Colonia</option><option value="12">Direccion</option><option value="13">CP</option><option value="14">Brick</option></select></th>';
                            valorColumna = valorColumna + 1;
                            codehtml = codehtml + '<th class="heading">' + option.valor + '</th>';
                        } else {
                            codehtml = codehtml + '<td>' + option.valor + '</td>';
                        }
                        filaanterior = fila;
                    });
                    codehtml = codehtml + '</tr></tbody></table>';
                    codehtml = codehtml.replace("[campos_nuevos]", opciones);
                    divCelldas.html(codehtml);
                    
                } else {
                    MostrarMensajeError(result.Cadena);
                }
                OcultarCargando();
            }
        });
    }
}




    function enviarInformacion() {
        //Realizamos el analisis de datos del mapa
        var columnas = 0;
        var loop = 0;
        var elementosMapa = "";
        while (loop == 0) {
            //Validamos si el elemento exist
            if ($('#ddlnoHoja' + columnas).length != 0) {
                var valorColumna = $('#ddlnoHoja' + columnas).val();
                if (valorColumna != 0) {
                    elementosMapa = elementosMapa + (columnas + 1 + "," + valorColumna + "_");
                }
            } else {
                loop = 1;
            }
            columnas = columnas + 1;
        }

        /* Validamos que la clave de mapa no esta vacia*/
        var nombreMapa = $("#claveMapa").val();
        if (nombreMapa.trim() == "") {
            MostrarMensajeError("Es obligatorio introducir el nombre del Mapa.");
        } else {
        elementosMapa = elementosMapa.substring(0, elementosMapa.length - 1);

        var x = document.getElementById("ddlnoHoja").value;
        var y = document.getElementById("nofila").value;
        var claveMapa = $("#claveMapa").val();
        
        if (x == "") {
            MostrarMensajeError("No se ha seleccionado la hoja de la que se obtendrán los datos.");
            return false;
        }

        $.ajax({
            type: "POST",
            url: "../Administracion/GuardarMapa",
            data: {
                "DatosExcel": elementosMapa,
                "ClaveMapa": claveMapa,
                "NoFilaHeaders": y,
                "hoja": x,
                "IsNuevo": true

            },
            cache: false,
            success: function (result) {
                if (result.ProcesoExitoso == 1) {
                    MostrarMensajeExito('Mapa guardado correctamente');
                    GoToAdministracionIndex(); 
                } else {
                    MostrarMensajeError(result.Mensaje);
                }
            }
        
            });
        }
    }