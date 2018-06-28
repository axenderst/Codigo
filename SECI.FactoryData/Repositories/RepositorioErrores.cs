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
    public class RepositorioErrores : BaseRepository
    {
        public List<ProcesoMapas> ConsultaErrores(int anio, int mes)
        {
            try
            {
                return FactoryProcesoMapas.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGetErroresByFecha", new DbParameter[]
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


        public int InsertaErrorMapa(int llproceso, string dsarchivo)
        {
            try
            {
                DbParameter llerror = DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llerror", DbType.Int32, DBNull.Value, -1, ParameterDirection.Output);
                    base._ProviderDB.ExecuteNonQuery("spInsErrores", new DbParameter[] {
                            llerror,
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.Int32, llproceso),
                            DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@dsarchivo", DbType.String, dsarchivo)
                    });
                return Convert.ToInt32(llerror.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



