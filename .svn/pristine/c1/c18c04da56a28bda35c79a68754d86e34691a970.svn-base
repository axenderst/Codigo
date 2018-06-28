using SECI.Entities;
using SECI.FactoryData.FactoryInstances;
using SECI.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Transactions;

namespace SECI.FactoryData.Repositories
{
    public class RepositorioProcesos : BaseRepository
    {

        public List<ProcesoMapas> ConsultaProcesoByFecha(int anio, int mes)
        {
            try
            {
                return FactoryProcesoMapas.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGetProcesoByFecha", new DbParameter[]
                  {
                    DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@noAnio", DbType.Int32, anio),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@noMes", DbType.Int32, mes)

                  }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ActualizaLogProcesoMapa(ProcesoMapas datosMapa)
        {
            try
            {
                return base._ProviderDB.ExecuteNonQuery("spUpdLogProcesoMapa", new DbParameter[]
                  {
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, datosMapa.llproceso),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llmapa", DbType.Int32, datosMapa.llmapa),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dslogMapa", DbType.String, datosMapa.dslogMapa),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llestatusMapa", DbType.String, datosMapa.llestatusMapa),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dsarchivo", DbType.String, datosMapa.dsarchivo)

                  });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        //    public Procesos ConProcesoMpLog(string dslog)
        //{
        //    try
        //    {
        //        return FactoryProcesosMapa.Get((DbDataReader)base._ProviderDB.GetDataReader("MostrarProcesoMapas", new DbParameter[]
        //          {
        //         DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@IdMapa", DbType.Int32, IdMapa),
        //         DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, llproceso)
        //          }));

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<Procesos> ConsultaProcesoMapaByIdproceso(int llproceso)
        {
            try
            {
                return FactoryProcesosMapa.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGetProcesoMapaByIdProceso", new DbParameter[]
                  {
                 DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, llproceso)

                  }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProcesoMapas> ConsultaAllProcesoMapaByIdproceso(int llproceso)
        {
            try
            {
                return FactoryProcesoMapas.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGetAllProcesoMapaByIdProceso", new DbParameter[]
                  {
                 DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, llproceso)

                  }));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



