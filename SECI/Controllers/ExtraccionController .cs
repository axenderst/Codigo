﻿using SECI.Entities;
using SECI.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using OfficeOpenXml;
using System.IO;
using System.Globalization;
using SECI.Common.FTP;
using System.Configuration;
using SECI.Common;

namespace SECI.Controllers
{
    public class ExtraccionController : Controller
    {

        #region Variables
        private List<ProcesoMapas> ListaProcesosMapas
        {
            get
            {
                return (List<ProcesoMapas>)Session["ListaProcesosMapas"];
            }
            set
            {
                Session["ListaProcesosMapas"] = value;
            }
        }

        //Trabajando con esta variable con el boton que se agrega
        private ProcesoMapas procesoTrabajo
        {
            get
            {
                return (ProcesoMapas)Session["procesoTrabajo"];
            }
            set
            {
                Session["procesoTrabajo"] = value;
            }
        }

        private ProcesoMapas mapaTrabajo
        {
            get
            {
                return (ProcesoMapas)Session["mapaTrabajo"];
            }
            set
            {
                Session["mapaTrabajo"] = value;
            }
        }

        private int procesoMapaCatalogo
        {
            get
            {
                return (int)Session["procesoMapaCatalogo"];
            }
            set
            {
                Session["procesoMapaCatalogo"] = value;
            }
        }

        #endregion

        // GET: Extraccion
        public ActionResult Index()
        {
            procesoTrabajo = new ProcesoMapas();
            return View();
        }

        public ActionResult ProcesarArchivo(String mesProceso)
        {
            Resultado<Catalogo> resultadoArchivo = new Resultado<Catalogo>();
            int IdPro = 0;
            int llmapa = 0;
            try
            {
                DateTime fechaProceso = Convert.ToDateTime(DateTime.ParseExact(mesProceso, "dd/MM/yyyy", CultureInfo.CurrentUICulture.DateTimeFormat));
                mapaTrabajo = new ProcesoMapas();
                //agregamos el .exe

                //Validar Campos de pantalla.
                Boolean isCorrecto = true;
                string mensajeError = "";
                //Obtenemos el prefigo del arvhivo
                resultadoArchivo.ProcesoExitoso = 0;
                if (Request.Files.Count == 0)
                {
                    isCorrecto = false;
                    resultadoArchivo.Mensaje = "Debes seleccionar un archivo para ejecutar.";
                    return Json(resultadoArchivo, JsonRequestBehavior.DenyGet);
                }

                string[] sufijo = Request.Files[0].FileName.Split('_');
                if (!Request.Files[0].FileName.ToUpper().EndsWith(".XLS") && !Request.Files[0].FileName.ToUpper().EndsWith(".XLSX")) {
                    isCorrecto = false;
                    mensajeError = "Seleccione un archivo correcto en formato Excel.";
                }

                //// Si no existe ningun mapa ejecutado mandamos mensaje para que ejecuten primero la carga mensual
                List<ProcesoMapas> procesomapa = FacadeProceso.ConsultaProcesoByFecha(fechaProceso.Date.Year, fechaProceso.Date.Month);
                if (procesomapa.Count == 0)
                {
                    //throw new Exception("No se ha encontrado ningún mapa, ejecute primero el proceso mensual para cargar los mapas.");
                    //1 Insert Procesos
                    Procesos proceso2 = new Procesos();
                    DateTime fechaHoy2 = DateTime.Now;
                    proceso2.boprocesomanual = true;
                    proceso2.fcinicio = fechaHoy2.ToString("dd/MM/yyyy");
                    proceso2.dslog = "Se inicia carga manual";
                    proceso2.dsmes = fechaProceso.Date.Month;
                    proceso2.dsanio = Convert.ToInt32(fechaProceso.Date.ToString("yyyy"));
                    IdPro = FacadeProceso.InsertarMvProceso(proceso2);
                }

                if (!isCorrecto)
                {
                    Procesos formato = new Procesos();
                    resultadoArchivo.Mensaje = mensajeError;
                    return Json(resultadoArchivo, JsonRequestBehavior.DenyGet);

                }


                string datoPrefijo = sufijo[0];


                //Obtenemos el mapa a cargar por el prefijo 
                List<Mapas> mapas = FacadeMapa.ObtenerMapasByPrefijo(datoPrefijo);

                //1 Insert Procesos
                Procesos proceso = new Procesos();
                DateTime fechaHoy = DateTime.Now;
                proceso.boprocesomanual = true;
                proceso.fcinicio = fechaHoy.ToString("dd/MM/yyyy");
                proceso.dslog = "Se inicia carga manual";
                proceso.dsmes = fechaProceso.Date.Month;
                proceso.dsanio = Convert.ToInt32(fechaProceso.Date.ToString("yyyy"));
                IdPro = FacadeProceso.InsertarMvProceso(proceso);

                if (mapas.Count == 0)
                {
                    //inserta log
                    Procesos lg = new Procesos();
                    proceso.dslog = ("Error sin ejecutar:");
                    resultadoArchivo.ProcesoExitoso = 3;
                    resultadoArchivo.Mensaje = "No se ha encontrado el mapa asociado";
                }
                else
                {
                    //Validamos si el mapa existe
                    if (procesomapa.Where(x => x.llproceso == IdPro && x.llmapa == mapas.First().mapaId && x.llestatusMapa == 2).Count() > 0) {
                        throw new Exception("El mapa ya se ha cargado para este proceso.");
                    }

                    //Cargamos el archivo en temporales
                    string rutaDocumento = HostingEnvironment.MapPath("~/Temporal/" + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(Request.Files[0].FileName));
                    Request.Files[0].SaveAs(rutaDocumento);

                    Mapas mapaExtraccion = mapas.First();

                    ActualizaProcesoMapa(IdPro, mapaExtraccion.mapaId, 4, Request.Files[0].FileName, "Inicia el proceso de extracción del archivo \"" + Request.Files[0].FileName + "\"");
                    ActualizaProcesoMapa(IdPro, mapaExtraccion.mapaId, 0, null, "Mapa encontrado \"" + mapaExtraccion.ClaveMapa + "\"");

                    mapaTrabajo.llproceso = IdPro;
                    mapaTrabajo.llmapa = mapaExtraccion.mapaId;

                    //2 inserta proceso_mapa mapa-proceso
                    Procesos proMapa = new Procesos();
                    proMapa.IdMapa = mapaExtraccion.mapaId;
                    proMapa.llprocesos = IdPro;
                    int IdPros = FacadeProceso.InsertaProcMapas(proMapa);



                    //Modificamos la seccion de tareas con EPPlus
                    //Creamos una nueva instancia con el archiv que utilizaremos como plantilla.
                    ExcelPackage pantillaExcel = new ExcelPackage(new FileInfo(rutaDocumento));

                    //Creamos un objecto tipo ExcelWorksheet que sera en la que vamos a trabajar.
                    ExcelWorksheet hojaTrabajo = pantillaExcel.Workbook.Worksheets[mapaExtraccion.hoja];

                    //Validamos los encabezados
                    string statusError = ValidarHeaders(hojaTrabajo, mapaExtraccion, IdPro);
                    if(statusError != String.Empty)
                    {
                        resultadoArchivo.ProcesoExitoso = 4;
                        resultadoArchivo.Mensaje = "Los encabezados no coinciden con el mapa configurado  <br/>" + statusError;
                        ActualizaProcesoMapa(IdPro, llmapa, 3, null, "La validación de encabezados terminó con errores.");
                        return Json(resultadoArchivo, JsonRequestBehavior.DenyGet);
                    }
                    //Datos del excel
                    //Llamamos la consulta del excel.
                    List<Catalogo> datosCatalogo = DatosExcel(hojaTrabajo, mapaExtraccion, IdPro);

                    // Actualizamos el estatus del mapa
                    ActualizaProcesoMapa(IdPro, mapaExtraccion.mapaId, 1, null, "Mapa procesado correctamente.");

                    resultadoArchivo.Lista = datosCatalogo;
                    resultadoArchivo.ProcesoExitoso = 1;
                }
            }
            catch (Exception ex)
            {
                //Actualizamos el estatus del mapa
                if (IdPro != 0 && llmapa != 0)
                {
                    ActualizaProcesoMapa(IdPro, llmapa, 3, null, ex.Message);
                }

                resultadoArchivo.ProcesoExitoso = 0;
                resultadoArchivo.Mensaje = ex.Message;
            }
            return Json(resultadoArchivo, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ProcesarCarpeta(String mesProceso)
        {

            Resultado<ProcesoMapas> resultadoArchivo = new Resultado<ProcesoMapas>();
            
            try
            {

                DateTime fechaProceso = Convert.ToDateTime(DateTime.ParseExact(mesProceso, "dd/MM/yyyy", CultureInfo.CurrentUICulture.DateTimeFormat));
                Procesos proceso = new Procesos();
                DateTime fechaHoy = DateTime.Now;
                proceso.boprocesomanual = false;
                proceso.dslog = "";
                proceso.dsmes = fechaProceso.Date.Month;
                proceso.dsanio = Convert.ToInt32(fechaProceso.Date.ToString("yyyy"));
                int IdPro = FacadeProceso.InsertarMvProceso(proceso);
                procesoTrabajo = new ProcesoMapas();
                procesoTrabajo.llproceso = IdPro;
                //Validamos el tipo de acceso a archivos.
                String tipoAccesoArchivos = ConfigurationManager.AppSettings["usarFTP"];
                List<string> archivos;
                if (tipoAccesoArchivos.Equals("true")) {
                    //Obtenemos los archivos del FTP
                    archivos = InteraccionFTP.obtenerListaArchivos(FacadeFTP.ConsultaFTP(), fechaProceso.Date.ToString("yyyy"), Convert.ToString(fechaProceso.Date.Month));
                } else
                {
                    //Obtenemos los datos de la carpeta y almacenamos los procedimientos de mapas.
                    string mes = Convert.ToString(fechaProceso.Date.Month);
                    if (fechaProceso.Date.Month < 10) {
                        mes = "0"+mes;
                    }
                    String[] archivosRuta = System.IO.Directory.GetFiles(ConfigurationManager.AppSettings["RutaDocumentos"] + fechaProceso.Date.ToString("yyyy") + "//" + mes );

                    archivos = new List<string>(archivosRuta);
                    
                }

                if (archivos.Count == 0)
                {
                    resultadoArchivo.ProcesoExitoso = 0;
                    resultadoArchivo.Mensaje = "No se han encontrado archivos a procesar.";
                    return Json(resultadoArchivo, JsonRequestBehavior.DenyGet);
                }

                //recorremos todos los archivos y los escribimos en consola
                List<ProcesoMapas> ListaProcesosMapas = new List<ProcesoMapas>();
                foreach (var rutaarchivo in archivos)
                {
                    Console.WriteLine(rutaarchivo);
                    String archivo = System.IO.Path.GetFileName(rutaarchivo);
                    ProcesoMapas procesoMapa = new ProcesoMapas();
                    procesoMapa.llproceso = IdPro;
                    procesoMapa.dsarchivo = archivo;
                    


                    //Obtenemos el prefigo del arvhivo
                    string[] sufijo = archivo.Split('_');
                    string datoPrefijo = sufijo[0];
                    //Obtenemos el mapa a cargar por el prefijo 
                    List<Mapas> mapas = FacadeMapa.BuscaMapa(datoPrefijo);


                    if (mapas.Count == 0)
                    {
                        // update log no hay mapa
                        procesoMapa.dslogMapa = "No se ha encontrado el mapa asociado";
                        procesoMapa.llestatusMapa = 3;
                        FacadeErrores.InsertaErrorMapa(IdPro, archivo);
                    }
                    else
                    {
                        //Verificamos si el mapa ya esta cargado en catalogo.
                        List<ProcesoMapas> mapa = FacadeProceso.ConsultaAllProcesoMapaByIdproceso(procesoTrabajo.llproceso).Where(x => x.llmapa == mapas.First().mapaId && x.llestatusMapa == 2).ToList();

                        if (mapa.Count() > 0)
                        {
                            procesoMapa.llmapa = mapa.First().llmapa;
                            procesoMapa.fcinicio = mapa.First().fcinicio;
                            procesoMapa.llestatusMapa = mapa.First().llestatusMapa;
                            procesoMapa.dsmapa = mapa.First().dsmapa;
                        }
                        else
                        {
                            //Cargamos el archivo en temporales
                            string rutaDocumento = HostingEnvironment.MapPath("~/Temporal/" + Guid.NewGuid().ToString() + System.IO.Path.GetExtension(archivo));

                            System.IO.File.Copy(rutaarchivo, rutaDocumento, true);
                            //Request.Files[0].SaveAs(rutaDocumento);

                            Mapas mapaExtraccion = mapas.First();

                            //si mapa extraccion es igual 86 imprime laa linea
                            Console.WriteLine(rutaarchivo);
                            procesoMapa.llmapa = mapaExtraccion.mapaId;
                            procesoMapa.dsmapa = mapaExtraccion.ClaveMapa;
                            procesoMapa.dslogMapa = "Inicia el proceso de extracción del archivo + " + archivo;
                            procesoMapa.dslogMapa = procesoMapa.dslogMapa + "Mapa encontrado" + mapaExtraccion.ClaveMapa;
                            //Inserta proceso mapa///////////////////////////////verificar
                            Procesos proMapa = new Procesos();
                            proMapa.IdMapa = mapaExtraccion.mapaId;
                            proMapa.llprocesos = IdPro;

                            //Datos del excel
                            List<Catalogo> datosCatalogo = new List<Catalogo>();
                            try
                            {
                                int IdPros = FacadeProceso.InsertaProcMapas(proMapa);
                                procesoMapa.fcinicio = DateTime.Now.ToString();
                                //Actualizamos el inicio del mapa
                                ActualizaProcesoMapa(IdPro, mapaExtraccion.mapaId, 4, archivo, "Inicia el proceso de extracción del archivo " + archivo);
                                ActualizaProcesoMapa(IdPro, mapaExtraccion.mapaId, 0, null, "Mapa encontrado " + mapaExtraccion.ClaveMapa);


                                //Modificamos la seccion de tareas con EPPlus
                                //Creamos una nueva instancia con el archiv que utilizaremos como plantilla.
                                ExcelPackage pantillaExcel = new ExcelPackage(new FileInfo(rutaDocumento));
                                //Creamos un objecto tipo ExcelWorksheet que sera en la que vamos a trabajar.
                                ExcelWorksheet hojaTrabajo = pantillaExcel.Workbook.Worksheets[mapaExtraccion.hoja];

                                //Validamos los encabezados
                                string statusError = ValidarHeaders(hojaTrabajo, mapaExtraccion, IdPro);
                                if (statusError != String.Empty)
                                {
                                    throw new Exception("La validación de encabezados terminó con errores.");
                                }

                                //Obtenemos los datos del exel
                                datosCatalogo = DatosExcel(hojaTrabajo, mapaExtraccion, IdPro);
                                procesoMapa.llestatusMapa = 1;
                                ActualizaProcesoMapa(IdPro, mapaExtraccion.mapaId, 1, null, "Mapa procesado correctamente.");
                            }
                            catch (Exception ex)
                            {
                                string errorMessage = ex.Message;
                                procesoMapa.dslogMapa = (procesoMapa.dslogMapa + errorMessage);
                                procesoMapa.llestatusMapa = 3;
                                //Actualizamos el estatus a error
                                ActualizaProcesoMapa(IdPro, mapaExtraccion.mapaId, 3, null, errorMessage);
                            }


                            procesoMapa.catalogos = datosCatalogo;
                        }
                    }
                    ListaProcesosMapas.Add(procesoMapa);
                }
                resultadoArchivo.Lista = ListaProcesosMapas;
                //Recuperamos los archivos que no se encontro mapa.
                resultadoArchivo.Listado = FacadeErrores.ConsultaErrores(fechaProceso.Date.Year, fechaProceso.Date.Month);
                foreach (ProcesoMapas mapaerror in ListaProcesosMapas.Where(x => x.llestatusMapa == 4 || x.llestatusMapa == 3).ToList())
                {
                    if(resultadoArchivo.Listado.Where(x=> x.llmapa == mapaerror.llmapa).Count() == 0)
                    {
                        resultadoArchivo.Listado.Add(mapaerror);
                    }
                    
                }
                

                
                resultadoArchivo.ProcesoExitoso = 1;
            }
            catch (Exception ex)
            {
                resultadoArchivo.ProcesoExitoso = 0;
                resultadoArchivo.Mensaje = ex.Message;
            }
            return Json(resultadoArchivo, JsonRequestBehavior.DenyGet);
        }

        private List<Catalogo> DatosExcel(ExcelWorksheet hojaTrabajo, Mapas mapaExtraccion, int idproceso)
        {

            int x = 1;
            int y = 0;
            int FilaInicial = mapaExtraccion.filaEncabezado + 1;
            List<Catalogo> catalogo = new List<Catalogo>();
            Boolean inserta = false;
            try {
                //Datos del excel
                while (x == 1)
                {
                    try
                    {
                        Catalogo Catalogo = new Catalogo() {
                            Brick = "",
                            Ciudad = "",
                            Colonia = "",
                            CP = "",
                            Direccion = "",
                            Estado = "",
                            Fecha = "",
                            Hospital = "",
                            Laboratorio = "",
                            Mayorista = "",
                            Medico = "",
                            Presentacion = "",
                            Sucursal = "",
                            Unidades = 0,
                            IdCatalog = 0,
                            Id_mapa = 0,
                            llproceso = idproceso
                        };
                        //Inicializamos variable en false
                        inserta = false;

                        if (mapaExtraccion.colMayorista != 0)
                        {
                            Catalogo.Mayorista = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colMayorista].Text;
                        }

                        if (mapaExtraccion.colPresentacion != 0)
                        {
                            Catalogo.Presentacion = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colPresentacion].Text;
                        }

                        if (mapaExtraccion.colFecha != 0)
                        {
                            object archivo = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colFecha].Value;
                            Catalogo.Fecha = (archivo == null ? "" : archivo.ToString());// hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colFecha].Value.ToString();
                        }

                        if (mapaExtraccion.colUnidades != 0)
                        {
                            string unida = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colUnidades].Text;

                            try
                            {
                                Catalogo.Unidades = Convert.ToDouble(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colUnidades].Text);
                                inserta = true;
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        if (mapaExtraccion.colMedico != 0)
                        {
                            Catalogo.Medico = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colMedico].Text;
                        }

                        if (mapaExtraccion.colEstado != 0)
                        {
                            Catalogo.Estado = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colEstado].Text;
                        }

                        if (mapaExtraccion.colHospital != 0)
                        {
                            Catalogo.Hospital = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colHospital].Text;
                        }

                        if (mapaExtraccion.colSucursal != 0)
                        {
                            Catalogo.Sucursal = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colSucursal].Text;
                        }

                        if (mapaExtraccion.colLaboratorio != 0)
                        {
                            Catalogo.Laboratorio = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colLaboratorio].Text;
                        }

                        if (mapaExtraccion.colCiudad != 0)
                        {
                            Catalogo.Ciudad = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colCiudad].Text;
                        }

                        if (mapaExtraccion.colColonia != 0)
                        {
                            Catalogo.Colonia = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colColonia].Text;
                        }

                        if (mapaExtraccion.colDireccion != 0)
                        {
                            Catalogo.Direccion = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colDireccion].Text;
                        }

                        if (mapaExtraccion.colCP != 0)
                        {
                            Catalogo.CP = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colCP].Text;
                        }
                        if (mapaExtraccion.colBrick != 0)
                        {
                            Catalogo.Brick = hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colBrick].Text;
                        }

                        Boolean final = ValidaFinalExcel(Catalogo);
                        //Validamos que ya no existan mas registros en la fila
                        if (final)
                        {
                            x = 0;
                        }

                        //2 inserta mapa temp
                        if (inserta == true && x == 1) {

                            Catalogo.Id_mapa = mapaExtraccion.mapaId;
                            FacadeCatalogoTem.InsertCatalogoTemp(Catalogo);
                            catalogo.Add(Catalogo);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    y++;
                    FilaInicial++;
                    /*if (y == 50)
                    {
                        x = 0;
                    }*/
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            return catalogo;
        }

        //consulta catalogo temp 
        public ActionResult DetalleProcesoMapa(int IdMapa, int llproceso, int llestatus)
        {
            Resultado<Catalogo> resultadoDetalle = new Resultado<Catalogo>();
            try
            {

                //Obtenemos el prefigo del arvhivo
                Catalogo muestraFile = new Catalogo();
                muestraFile.Id_mapa = IdMapa;
                muestraFile.llproceso = llproceso;

                List<Catalogo> proceso = new List<Catalogo>();
                //Obtenemos el mapa a cargar por el prefijo 
                if (llestatus == Constantes.ID_ESTATUS_CARGADO)
                {
                    proceso = FacadeCatalogo.ConsultaCatalogoByProcesoMapa(llproceso, IdMapa);
                }
                else if (llestatus == Constantes.ID_ESTATUS_PROCESADO)
                {
                    proceso = FacadeCatalogoTem.ObtenerCatalogoTem(muestraFile);
                }

                resultadoDetalle.ProcesoExitoso = 1;
                resultadoDetalle.Lista = proceso;
            }
            catch (Exception ex)
            {
                resultadoDetalle.ProcesoExitoso = 0;
                resultadoDetalle.Mensaje = ex.Message;
            }
            return Json(resultadoDetalle, JsonRequestBehavior.DenyGet);
        }

        //Copiar datos de tabla CatalogoTemp a catalogo
        public ActionResult GuardarCatalogo()
        {
            Resultado<Catalogo> resultado = new Resultado<Catalogo>();
            //int Id_Catalogo, int Id_mapa,int llproceso
            try
            {

                Catalogo catalogo = new Catalogo();
                catalogo.Id_mapa = mapaTrabajo.llmapa;// Id_mapa;
                catalogo.llproceso = mapaTrabajo.llproceso;

                FacadeCatalogo.InsertaCatalogo2(catalogo);
                //ActualizaProcesoMapa(mapaTrabajo.llproceso, mapaTrabajo.llmapa, 2, null, "Los datos han sido cargados correctamente al CATALOGO.");
                resultado.ProcesoExitoso = 1;

            }
            catch (Exception e)
            {
                resultado.ProcesoExitoso = 0;
                resultado.Mensaje = e.Message;
                ActualizaProcesoMapa(mapaTrabajo.llproceso, mapaTrabajo.llmapa, 3, null, e.Message);
            }
            return Json(resultado, JsonRequestBehavior.DenyGet);

        }

        public ActionResult ConsultarProceso(String mesProceso)
        {
            Resultado<ProcesoMapas> resultadoDetalle = new Resultado<ProcesoMapas>();
            //Inicializamos la variable que representa el proceso ejecutado actualmente.
            procesoTrabajo = new ProcesoMapas();
            try
            {
                DateTime fechaProceso = Convert.ToDateTime(DateTime.ParseExact(mesProceso, "dd/MM/yyyy", CultureInfo.CurrentUICulture.DateTimeFormat));

                //Consultamos los procesos erroneos
                List<ProcesoMapas> ProcesoError = FacadeErrores.ConsultaErrores(fechaProceso.Date.Year, fechaProceso.Date.Month);
                resultadoDetalle.Listado = ProcesoError.ToList();
                //Agregamos los errores a la vista principal
                resultadoDetalle.Lista = ProcesoError.ToList();

                //Consultamos los datos en base de datos
                List<ProcesoMapas> mapasProceso = FacadeProceso.ConsultaProcesoByFecha(fechaProceso.Date.Year, fechaProceso.Date.Month);

                resultadoDetalle.Listado.AddRange(mapasProceso.Where(x => x.llestatusMapa == Constantes.ID_ESTATUS_INICIADO || x.llestatusMapa == Constantes.ID_ESTATUS_ERROR).ToList());
                resultadoDetalle.Lista.AddRange(mapasProceso.ToList());
                resultadoDetalle.Lista = resultadoDetalle.Lista.OrderBy(x => x.fcinicio).ToList();

                // Si no existen registros agregamos como proceso exitoso 2 para indicar que no se cargen los GRIDS
                if (mapasProceso.Count == 0)
                {
                    resultadoDetalle.ProcesoExitoso = 2;
                }
                else
                {
                    procesoTrabajo.llproceso = mapasProceso[0].llproceso;
                    resultadoDetalle.ProcesoExitoso = 1;
                }
            }
            catch (Exception ex)
            {
                resultadoDetalle.ProcesoExitoso = 0;
                resultadoDetalle.Mensaje = ex.Message;
            }
            return Json(resultadoDetalle, JsonRequestBehavior.DenyGet);
        }

        public void ConsultaLog(string dslog)
        {
            Resultado<Procesos> resultadoArchivo = new Resultado<Procesos>();
            try
            {
                ProcesoMapas verLog = new ProcesoMapas();
                verLog.dslogMapa = dslog;

                FacadeProceso.ActualizaLogProcesoMapa(verLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
    }



        private void ActualizaProcesoMapa(int llproceso, int llmapa, int llestatus, string dsarchivo, string dslog)
        {
            try
            {
                ProcesoMapas updateProceso = new ProcesoMapas();
                updateProceso.llproceso = llproceso;
                updateProceso.llmapa = llmapa;
                updateProceso.llestatusMapa = llestatus;
                updateProceso.dsarchivo = dsarchivo;
                updateProceso.dslogMapa = dslog;

                FacadeProceso.ActualizaLogProcesoMapa(updateProceso);
            }catch(Exception ex)
            {
                throw ex;
            }

        }

        //public ActionResult ConsultaProcesoMapaByIdproceso(int procesoMapaCatalogo)
        //{
        //}

        public Boolean ValidaFinalExcel(Catalogo catalogo)
        {
            Boolean isUltima = false;
            try
            {
                if(catalogo.Brick == "" && catalogo.Ciudad == "" && catalogo.Colonia == "" && catalogo.CP == "" &&
                    catalogo.Direccion == "" && catalogo.Estado == "" && catalogo.Fecha == "" && catalogo.Hospital == "" &&
                    catalogo.Laboratorio == "" && catalogo.Mayorista == "" && catalogo.Medico == "" && catalogo.Presentacion == "" &&
                    catalogo.Sucursal == "" && catalogo.Unidades == 0)
                {
                    isUltima = true;
                }
                return isUltima;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public ActionResult GuardarCatalogoMensual()
        {
            Resultado<string> resultado = new Resultado<string>();
            try
            {  
                //Consultamos los mapas erroneos con el proceso guradado en la variable de session Variable de sesion
                int noError = FacadeProceso.ConsultaAllProcesoMapaByIdproceso(procesoTrabajo.llproceso).Where(x=> x.llestatusMapa == 3 || x.llestatusMapa == 4).Count();
                if(noError > 0)
                {
                    throw new Exception("Tienes archivos sin un mapa asociado o que necesitan ser modificados.");
                }
                List<Procesos> procesoMapa = FacadeProceso.ConsultaProcesoMapaByIdproceso(procesoTrabajo.llproceso);
                //Ejecutamos el proceso de guardado mensual a catalogo
                FacadeCatalogo.InsertaCatalogoMensual(procesoMapa);
                resultado.ProcesoExitoso = 1;
            }
            catch (Exception ex)
            {
                resultado.ProcesoExitoso = 0;
                resultado.Mensaje = ex.Message;
                
            }
            return Json(resultado, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ConsultarLog(int llproceso, int llmapa)
        {
            Resultado<string> resultado = new Resultado<string>();
            try
            {   if (llmapa == 0)
                {
                    resultado.Mensaje = "No se ha encontrado el mapa asociado";
                }
                else
                {
                    resultado.Mensaje = FacadeProceso.ConsultaProcesoMapaByIdproceso(procesoTrabajo.llproceso).Where(x => x.IdMapa == llmapa).First().dslog;
                    resultado.Mensaje = resultado.Mensaje.Replace("\r", "<br />");
                }
                resultado.ProcesoExitoso = 1;
            }
            catch (Exception ex)
            {
                resultado.ProcesoExitoso = 0;
                resultado.Mensaje = ex.Message;

            }
            return Json(resultado, JsonRequestBehavior.DenyGet);
        }

        public string ValidarHeaders(ExcelWorksheet hojaTrabajo, Mapas mapaExtraccion, int idproceso)
        {
            string ErrorHeader = "";
            try
            {
                EncabezadoMapas datosencabezado = FacadeMapa.ConsultaEncabezadoByllmapa(mapaExtraccion.mapaId);
                int FilaInicial = mapaExtraccion.filaEncabezado;
                Boolean isCorrectHeaders = true;
                string HeaderArchivo = "";
                string HeaderMapa = "";
                if (mapaExtraccion.colMayorista != 0) {
                    //Cargamos los datos a validar.
                    HeaderMapa = limpiaHeader(datosencabezado.colMayorista);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colMayorista].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader +  Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colPresentacion != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colPresentacion);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colPresentacion].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader +  Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colFecha != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colFecha);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colFecha].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader +  Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colUnidades != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colUnidades);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colUnidades].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader +  Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colMedico != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colMedico);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colMedico].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader +  Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colEstado != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colEstado);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colEstado].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader +  Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colHospital != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colHospital);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colHospital].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader + Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colSucursal != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colSucursal);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colSucursal].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader + Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colLaboratorio != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colLaboratorio);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colLaboratorio].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader + Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colCiudad != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colCiudad);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colCiudad].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader + Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colColonia != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colColonia);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colColonia].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader + Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colDireccion != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colDireccion);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colDireccion].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader + Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }

                if (mapaExtraccion.colCP != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colCP);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colCP].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader + Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                }
                if (mapaExtraccion.colBrick != 0)
                {
                    HeaderMapa = limpiaHeader(datosencabezado.colBrick);
                    HeaderArchivo = limpiaHeader(hojaTrabajo.Cells[FilaInicial, mapaExtraccion.colBrick].Text);
                    if (HeaderArchivo != HeaderMapa)
                    {
                        isCorrectHeaders = false;
                        //Actualizamos el log del mapa con el error
                        ErrorHeader = ErrorHeader + Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa) + " <br />";
                        ActualizaProcesoMapa(idproceso, mapaExtraccion.mapaId, 0, null, Constantes.MESAJE_ERROR_HEADERS_MAPA.Replace("[HEADER_AR]", HeaderArchivo).Replace("[HEADER_MAP]", HeaderMapa));
                    }
                } 
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ErrorHeader;
        }

        private string limpiaHeader(string header)
        {
            return header.ToUpper().Trim();
        }

    }
}