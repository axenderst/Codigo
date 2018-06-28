using SECI.Entities;
using SECI.FactoryData.FactoryInstances;
using SECI.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace SECI.FactoryData.Repositories
{
    public class RepositorioConsultas : BaseRepository
    {


        //------------------------------Generación de consultas en mapas tabla CtMapas--------------------------------------------------------------------
        #region Generación de consultas en mapas tabla CtMapas
        //Inserta nuevos mapas
        public int InsertaMapa(Mapas Mapas, EncabezadoMapas encabezadoMapa)
        {
            try
            {
                DbParameter ldMapa = DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB,
                "@IdCatMapa", DbType.Int32, DBNull.Value, -1, ParameterDirection.Output);
                using (TransactionScope scope = new TransactionScope())
                {
                    base._ProviderDB.ExecuteNonQuery("spIns_Mapa", new DbParameter[] {
                            ldMapa,
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@ClaveMapa", DbType.String, Mapas.ClaveMapa),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Hoja", DbType.Int32, Mapas.hoja),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@FilaEncabezado", DbType.Int32, Mapas.filaEncabezado),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Mayorista", DbType.Int32, Mapas.colMayorista),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Presentacion", DbType.Int32, Mapas.colPresentacion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Fecha", DbType.Int32, Mapas.colFecha),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Unidades", DbType.Int32, Mapas.colUnidades),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Medico", DbType.Int32, Mapas.colMedico),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Estado", DbType.Int32, Mapas.colEstado),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Hospital", DbType.Int32, Mapas.colHospital),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Sucursal", DbType.Int32, Mapas.colSucursal),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Laboratorio", DbType.Int32, Mapas.colLaboratorio),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Ciudad", DbType.Int32, Mapas.colCiudad),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Colonia", DbType.Int32, Mapas.colColonia),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Direccion", DbType.Int32, Mapas.colDireccion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_CP", DbType.Int32, Mapas.colCP),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Brick", DbType.Int32, Mapas.colBrick),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dsarchivo", DbType.String, Mapas.dsarchivo)
                    });

                    base._ProviderDB.ExecuteNonQuery("spIns_CtEncabezadoMapa", new DbParameter[] {
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@IdMapa", DbType.Int32, ldMapa.Value),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Mayorista", DbType.String, encabezadoMapa.colMayorista),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Presentacion", DbType.String, encabezadoMapa.colPresentacion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Fecha", DbType.String, encabezadoMapa.colFecha),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Unidades", DbType.String, encabezadoMapa.colUnidades),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Medico", DbType.String, encabezadoMapa.colMedico),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Estado", DbType.String, encabezadoMapa.colEstado),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Hospital", DbType.String, encabezadoMapa.colHospital),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Sucursal", DbType.String, encabezadoMapa.colSucursal),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Laboratorio", DbType.String, encabezadoMapa.colLaboratorio),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Ciudad", DbType.String, encabezadoMapa.colCiudad),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Colonia", DbType.String, encabezadoMapa.colColonia),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Direccion", DbType.String, encabezadoMapa.colDireccion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_CP", DbType.String, encabezadoMapa.colCP),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Brick", DbType.String, encabezadoMapa.colBrick)
                    });
                    scope.Complete();
                }
                return Convert.ToInt32(ldMapa.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Muestra todos los datos almacenados en mapa
        public List<Mapas> mostrarMapas()
        {
            try
            {
                return FactoryMapa.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGet_mostrarMapas", new DbParameter[] { }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Busca un mapa de acuerdo a claveMapa igual escrito
        public List<Mapas> BuscaMps(string ClaveMapa)
        {
            try
            {
                return FactoryMapaCompleto.GetList((DbDataReader)base._ProviderDB.GetDataReader("sp_buscarSufijo", new DbParameter[]
                      {
                    DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@ClaveMapa", DbType.String, ClaveMapa)
                      }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Busca un mapa de acuerdo a claveMapa sin emportar que el campo este vacio, dado el caso muestra todo
        public List<Mapas> ConsultaMapasByPrefijo(string ClaveMapa)
        {
            try
            {
                return FactoryMapaCompleto.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGetMapasBySufijo", new DbParameter[]
                {
               DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@ClaveMapa", DbType.String, ClaveMapa)
                }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Busca un mapa de acuerdo a claveMapa sin emportar que el campo este vacio, dado el caso muestra todo
        public Mapas ObtenerMapasById(int IdMapa)
        {
            try
            {
                return FactoryMapaCompleto.Get((DbDataReader)base._ProviderDB.GetDataReader("spGetMapasByIdMapa", new DbParameter[]
                {
               DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@IdMapa", DbType.Int32, IdMapa)
                }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Busca un mapa que sea igual al nombre introducido sin importar may/min/caracteres especificos
        public List<Mapas> examinaMapa(string ClaveMapa)
        {
            try
            {
                return FactoryMapaCompleto.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGetVerificarMapa", new DbParameter[]
                  {
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@ClaveMapa", DbType.String, ClaveMapa)
                  }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Actualización de la tabla CtMapas
        public int updateMapas(Mapas upMapa, EncabezadoMapas encabezadoMapa)
        {
            int resultado1 = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    base._ProviderDB.ExecuteNonQuery("spUpdCt_Mapas", new DbParameter[] {
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@IdMapa", DbType.String, upMapa.mapaId),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@ClaveMapa", DbType.String, upMapa.ClaveMapa),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Hoja", DbType.Int32, upMapa.hoja),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@FilaEncabezado", DbType.Int32, upMapa.filaEncabezado),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Mayorista", DbType.Int32, upMapa.colMayorista),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Presentacion", DbType.Int32, upMapa.colPresentacion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Fecha", DbType.Int32, upMapa.colFecha),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Unidades", DbType.Int32, upMapa.colUnidades),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Medico", DbType.Int32, upMapa.colMedico),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Estado", DbType.Int32, upMapa.colEstado),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Hospital", DbType.Int32, upMapa.colHospital),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Sucursal", DbType.Int32, upMapa.colSucursal),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Laboratorio", DbType.Int32, upMapa.colLaboratorio),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Ciudad", DbType.Int32, upMapa.colCiudad),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Colonia", DbType.Int32, upMapa.colColonia),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Direccion", DbType.Int32, upMapa.colDireccion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_CP", DbType.Int32, upMapa.colCP),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Brick", DbType.Int32, upMapa.colBrick),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dsarchivo", DbType.String, upMapa.dsarchivo)
                    });

                    base._ProviderDB.ExecuteNonQuery("spUpd_EncabMapas", new DbParameter[] {
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@IdMapa", DbType.Int32, upMapa.mapaId),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Mayorista", DbType.String, encabezadoMapa.colMayorista),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Presentacion", DbType.String, encabezadoMapa.colPresentacion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Fecha", DbType.String, encabezadoMapa.colFecha),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Unidades", DbType.String, encabezadoMapa.colUnidades),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Medico", DbType.String, encabezadoMapa.colMedico),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Estado", DbType.String, encabezadoMapa.colEstado),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Hospital", DbType.String, encabezadoMapa.colHospital),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Sucursal", DbType.String, encabezadoMapa.colSucursal),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Laboratorio", DbType.String, encabezadoMapa.colLaboratorio),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Ciudad", DbType.String, encabezadoMapa.colCiudad),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Colonia", DbType.String, encabezadoMapa.colColonia),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Direccion", DbType.String, encabezadoMapa.colDireccion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_CP", DbType.String, encabezadoMapa.colCP),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Col_Brick", DbType.String, encabezadoMapa.colBrick)
                    });


                    scope.Complete();
                };
                resultado1 = upMapa.mapaId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado1;
        }

        #endregion

        //-------------------------------Generación de consultas procesos tabla mvprocesos--------------------------------------------------------------------

        //Muestra todos los procesos en mvprocesos
        public List<Procesos> MuestraProceso()
        {
            try
            {
                return FactoryProcesos.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGet_MostrarProceso", new DbParameter[]
                  { }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Inserta un nuevo proceso en tabla mvprocesos
        public int InsertaProcesomv(Procesos proceso)
        {
            try
            {
                DbParameter llprocesos = DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB,
                "@llproceso", DbType.Int32, DBNull.Value, -1, ParameterDirection.Output);
                using (TransactionScope scope = new TransactionScope())
                {
                    base._ProviderDB.ExecuteNonQuery("spIns_proceso", new DbParameter[]
                    {
                         llprocesos,
                         DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@boprocesomanual", DbType.Boolean, proceso.boprocesomanual),
                         DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dslog", DbType.String, proceso.dslog),
                         DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dsanio", DbType.String, proceso.dsanio),
                         DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dsmes", DbType.String, proceso.dsmes)
                    });
                    scope.Complete();
                }
                return Convert.ToInt32(llprocesos.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Actuaización de la tabla mvprocesos ingresndo los Logs "Movimientos" mediante columna llproceso
        public int UpProcesos(Procesos upproceso)
        {
            int resultado = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    base._ProviderDB.ExecuteNonQuery("spUpd_MvProce", new DbParameter[]
                    {
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, upproceso.boprocesomanual),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dslog", DbType.String, upproceso.dslog)
                     });

                    scope.Complete();
                };
                resultado = upproceso.llprocesos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }





        //-------------------------------Generación de consultas procesos tabla proceso_mapas--------------------------------------------------------------------

        //Inserta los procesos realizados en mapas en tabla  proceso_mapas
        public int InsertaProcesoMapas(Procesos procMapa)
        {
            int resultado1 = 0;
            int resultado2 = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    base._ProviderDB.ExecuteNonQuery("spInsprocesoMapas", new DbParameter[]
                    {
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@IdMapa", DbType.Int32, procMapa.IdMapa),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.String, procMapa.llprocesos)
                     });
                    scope.Complete();
                };
                resultado1 = procMapa.IdMapa;
                resultado2 = procMapa.llprocesos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado1;
        }






        //---------------------------------Generación de consultas procesos tabla Catalogo_Temp--------------------------------------------------------------------

        //Inserta un nuevo catalogo temporal en Catalogo_Temp
        public int InsertaCatalogoTem(Catalogo Catalogo)
        {
            try
            {
                DbParameter ldCatalogo = DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB,
                "@IdCatalog", DbType.Int32, DBNull.Value, -1, ParameterDirection.Output);
                using (TransactionScope scope = new TransactionScope())
                {
                    //spIns_Catalogo---remplazar por Tem 
                    base._ProviderDB.ExecuteNonQuery("spIns_CatalogoTem", new DbParameter[] {
                        //IdCat
                            ldCatalogo,
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Mayorista", DbType.String, Catalogo.Mayorista),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Presentacion", DbType.String, Catalogo.Presentacion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Fecha", DbType.String, Catalogo.Fecha),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Unidades", DbType.String, Catalogo.Unidades),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Medico", DbType.String, Catalogo.Medico),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Estado", DbType.String, Catalogo.Estado),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Hospital", DbType.String, Catalogo.Hospital),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Sucursal", DbType.String, Catalogo.Sucursal),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Laboratorio", DbType.String, Catalogo.Laboratorio),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@ciudad", DbType.String, Catalogo.Ciudad),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Colonia", DbType.String, Catalogo.Colonia),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Direccion", DbType.String, Catalogo.Direccion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@CP", DbType.String, Catalogo.CP),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Brick", DbType.String, Catalogo.Brick),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Id_mapa", DbType.String, Catalogo.Id_mapa),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.String, Catalogo.llproceso),
                    });
                    scope.Complete();
                    return Convert.ToInt32(ldCatalogo.Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //---------------------------------Generación de consultas procesos tabla Catalogo_--------------------------------------------------------------------

        //Inserta un nuevo catalogo temporal en Catalogo_
        public int InsertaCatalogo(Catalogo Catalogo)
        {
            try
            {
                DbParameter ldCatalogo = DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB,
                "@IdCatalog", DbType.Int32, DBNull.Value, -1, ParameterDirection.Output);
                using (TransactionScope scope = new TransactionScope())
                {
                    //spIns_Catalogo---remplazar por Tem 
                    base._ProviderDB.ExecuteNonQuery("spIns_Catalogo", new DbParameter[] {
                        //IdCat
                            ldCatalogo,
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Mayorista", DbType.String, Catalogo.Mayorista),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Presentacion", DbType.String, Catalogo.Presentacion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Fecha", DbType.String, Catalogo.Fecha),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Unidades", DbType.String, Catalogo.Unidades),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Medico", DbType.String, Catalogo.Medico),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Estado", DbType.String, Catalogo.Estado),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Hospital", DbType.String, Catalogo.Hospital),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Sucursal", DbType.String, Catalogo.Sucursal),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Laboratorio", DbType.String, Catalogo.Laboratorio),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Ciudad", DbType.String, Catalogo.Ciudad),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Colonia", DbType.String, Catalogo.Colonia),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Direccion", DbType.String, Catalogo.Direccion),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@CP", DbType.String, Catalogo.CP),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Brick", DbType.String, Catalogo.Brick),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Id_mapa", DbType.String, Catalogo.Id_mapa),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.String, Catalogo.llproceso),
                    });
                    scope.Complete();
                    return Convert.ToInt32(ldCatalogo.Value);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int InsertaCatalogo2(Catalogo Catalogo)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //Inserta los datos en catalogo
                    base._ProviderDB.ExecuteNonQuery("spInsCatalogo2", new DbParameter[] {

                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, Catalogo.llproceso),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Id_mapa", DbType.Int32, Catalogo.Id_mapa)
                    });

                   // Borramos los datos de catalogotemp
                   base._ProviderDB.ExecuteScalar("spDelCatalogoTempByProcesoMapa",
                    new DbParameter[] {
                        DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB,"@llproceso",DbType.Int32, Catalogo.llproceso),
                        DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB,"@Id_mapa",DbType.String, Catalogo.Id_mapa)
                    });

                  //Actualizamos el estatus del mapa
                  base._ProviderDB.ExecuteNonQuery("spUpdLogProcesoMapa", new DbParameter[]
                     {
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, Catalogo.llproceso),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llmapa", DbType.Int32, Catalogo.Id_mapa),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dslogMapa", DbType.String, "Los datos han sido cargados correctamente al CATALOGO."),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llestatusMapa", DbType.String, 2),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dsarchivo", DbType.String, null)

                    });

                    scope.Complete();

                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }


        public int InsertaCatalogoMensual(List<Procesos> procesoMapa)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (Procesos proceso in procesoMapa)
                    {
                        Catalogo catalogo = new Catalogo();
                        catalogo.Id_mapa = proceso.IdMapa;
                        catalogo.llproceso = proceso.llprocesos;


                        //Inserta los datos en catalogo
                        base._ProviderDB.ExecuteNonQuery("spInsCatalogo2", new DbParameter[] {

                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, catalogo.llproceso),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Id_mapa", DbType.Int32, catalogo.Id_mapa)
                    });

                        // Borramos los datos de catalogotemp
                        base._ProviderDB.ExecuteScalar("spDelCatalogoTempByProcesoMapa",
                         new DbParameter[] {
                        DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB,"@llproceso",DbType.Int32, catalogo.llproceso),
                        DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB,"@Id_mapa",DbType.String, catalogo.Id_mapa)
                         });

                        //Actualizamos el estatus del mapa
                        base._ProviderDB.ExecuteNonQuery("spUpdLogProcesoMapa", new DbParameter[]
                           {
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, catalogo.llproceso),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llmapa", DbType.Int32, catalogo.Id_mapa),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dslogMapa", DbType.String, "Los datos han sido cargados correctamente al CATALOGO."),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llestatusMapa", DbType.String, 2),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dsarchivo", DbType.String, null)

                          });
                    }
                    scope.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
               
            }
        }


    }
}



