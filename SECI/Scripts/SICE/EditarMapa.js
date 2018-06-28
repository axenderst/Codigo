$(document).ready(function () {
    //Consultamos los datos del excel y asignamos la hoja seleccionada
    spinner();
        $.ajax({
            type: "POST",
            url: "../Administracion/AnalisisPlantillaExcel",
            data:{"IsModificacion": true},
            cache: false,
            success: function (result) {
                if (result.ProcesoExitoso == 1) {
                    var ddlHojas = $("#ddlnoHoja");
                    ddlHojas.html('');
                    $.each(result.Lista, function (id, option) {
                        if (option.noHoja == result.Numero) {
                            ddlHojas.append($('<option  selected="selected"></option>').val(option.noHoja).html(option.Nombre));
                        } else {
                            ddlHojas.append($('<option></option>').val(option.noHoja).html(option.Nombre));
                        }
                    });
                    //Cargamos los datos del archivo en la hoja seleccionada
                    SeleccionHojaInicio();
                } else {
                    MostrarMensajeError(result.Cadena);
                }
                OcultarCargando();
            }
        });

});

function GoToBuscar() {
    ObtenerRegistrosSeguimiento();
}

function archivoCargado() {
    //Obtenemos los datos del archivo para enviarlo al servidor
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
            } else {
                MostrarMensajeError(result.Cadena);
            }
        }
    });
}

    function ObtenerRegistrosSeguimiento() {
        // MostrarCargando();
        spinner();
        $.ajax({
            type: "POST",
            url: "../Mapa/ListarMapas",
            data: {
            //    Mapa: $("#numMapa").val(),
            //    Sufijo: $("#nomMapa").val(),
            //    Hoja: $("#hojaBase").val(),
            //    Encabezado: $("#filaEncabezado").val(),
            //    FechaCreacion: $("#fechaCreacion").val(),
            //    FechaModif: $("#fechaUltimaModif").val()

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
                    OcultarCargando();
                }
                else {
                    OcultarCargando();
                    MostrarMensajeError('Error' + result.Mensaje);
                    //MostrarMensajeError('Error' + result.Mensaje);
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
        
        //alert('valor de fila = ' + y);
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
                                codehtml = codehtml + '<td align="left" valign="bottom" class="heading">' + (fila) +'</td>';
                            } else {
                                codehtml = codehtml + '<td align="left" valign="bottom" class="heading">' + (fila) + '</td>';
                            }

                            
                            primeraFila = 1;
                        }
                        if (fila == y) {
                            opciones = opciones + '<th  class="heading"><select style="width:110px;" class="form-control" name="ddlnoHoja'+valorColumna+'" id="ddlnoHoja'+valorColumna+'"><option value="0">Seleccione</option><option value="1">Mayorista</option><option value="2">Presentacion</option><option value="3">Fecha</option><option value="4">Unidades</option><option value="5">Medico</option><option value="6">Estado</option><option value="7">Hospital</option><option value="8">Sucursal</option><option value="9">Laboratorio</option><option value="10">Ciudad</option><option value="11">Colonia</option><option value="12">Direccion</option><option value="13">CP</option><option value="14">Brick</option></select></th>';
                            valorColumna = valorColumna + 1;
                            codehtml = codehtml + '<th class="heading">' + option.valor + '</th>';
                        } else {
                            codehtml = codehtml + '<td>' + option.valor + '</td>';
                        } 
                            filaanterior = fila;
                            });
                    codehtml = codehtml + '</tr></tbody></table>';
                    codehtml = codehtml.replace("[campos_nuevos]", opciones);
                    
                        //alert(codehtml);
                        divCelldas.html(codehtml);
                            } else {
                    MostrarMensajeError(result.Cadena);
                }
            }
        });

    }


    function CargarConfiguracion() {
        $.ajax({
            type: "POST",
            url: "../Administracion/CargarConfiguracion",
            data: {},
            cache: false,
            success: function (result) {
                if (result.ProcesoExitoso == 1) {
                    //Cargamos la configuracion del mapa 
                     if (result.Objeto.colMayorista != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colMayorista - 1), 1);
                    } if (result.Objeto.colPresentacion != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colPresentacion - 1), 2);
                    } if (result.Objeto.colFecha != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colFecha - 1), 3);
                    } if (result.Objeto.colUnidades != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colUnidades - 1), 4);
                    } if (result.Objeto.colMedico != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colMedico - 1), 5);
                    } if (result.Objeto.colEstado != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colEstado - 1), 6);
                    } if (result.Objeto.colHospital != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colHospital - 1), 7);
                    } if (result.Objeto.colSucursal != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colSucursal - 1), 8);
                    } if (result.Objeto.colLaboratorio != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colLaboratorio - 1), 9);
                    } if (result.Objeto.colCiudad != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colCiudad - 1), 10);
                    } if (result.Objeto.colColonia != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colColonia - 1), 11);
                    } if (result.Objeto.colDireccion != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colDireccion - 1), 12);
                    } if (result.Objeto.colCP != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colCP - 1), 13);
                    } if (result.Objeto.colBrick != 0) {
                        SelectElement("ddlnoHoja" + (result.Objeto.colBrick - 1), 14);
                    }


                } else {
                    MostrarMensajeError(result.Cadena);
                }
            }
        });

    }

    function SelectElement(idelemento, valueToSelect) {
        var element = document.getElementById(idelemento);
        element.value = valueToSelect;
    }

function ArchivoSeleccionado() {
    //Obtenemos los datos del archivo para enviarlo al servidor
    spinner();
    // Limpiamos y grid de excel
    $("#ddlnoHoja").html('');
    $("#datosExcel").html('');
    $("#claveMapa").val('');
    $("#nofila").val('1');

    console.log("Termina la limpieza de los campos");
    var formData = new FormData();
    formData.append("FileUpload", document.getElementById('flArchivo2').files[0]);


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

    function CambiarArchivo() {
        var configurado = document.getElementById("divArchivoConfigurado");
        var cambiar = document.getElementById("divCambiaArchivo");

        cambiar.style.display = "block";
        configurado.style.display = "none";
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

        elementosMapa = elementosMapa.substring(0, elementosMapa.length - 1);

        var x = document.getElementById("ddlnoHoja").value;
        var y = document.getElementById("nofila").value;
        var claveMapa = $("#claveMapa").val()

        $.ajax({
            type: "POST",
            url: "../Administracion/GuardarMapa",
            data: {
                "DatosExcel": elementosMapa,
                "ClaveMapa": claveMapa,
                "NoFilaHeaders": y,
                "hoja": x,
                "IsNuevo": false
            },
            cache: false,
            success: function (result) {
                if (result.ProcesoExitoso == 1) {
                    MostrarMensajeExito('Mapa guardado correctamente');
                } else {
                    MostrarMensajeError(result.Mensaje);
                }
            }
        });
    }

   // se realiza la seleccion de hoja y carga de los datos ya seleccionados
    function SeleccionHojaInicio() {
        var x = document.getElementById("ddlnoHoja").value;
        var y = document.getElementById("nofila").value;

        //alert('valor de fila = ' + y);
        $.ajax({
            type: "POST",
            url: "../Administracion/CargarDatosExcel",
            data: { "noHoja": x, "noFila": y },
            cache: false,
            async: false,
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

                    //alert(codehtml);
                    divCelldas.html(codehtml);

                    //Cargamos la configuracion del mapa
                    CargarConfiguracion();
                } else {
                    MostrarMensajeError(result.Cadena);
                }
            }
        });

    }