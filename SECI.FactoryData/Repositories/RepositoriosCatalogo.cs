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
    public class RepositoriosCatalogo : BaseRepository
    {
        //Muestra los datos que se almacenaron en la tabla catalogo temporal realizando busqueda por idmapa y llproceso
        public List<Catalogo> ConsultaCatalogoByProcesoMapa(int llproceso, int llmapa)
        {
            try
            {
                return FactoryCatalog.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGetCatalogoByProcesoMapa", new DbParameter[]
              {
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llmapa", DbType.String, llmapa),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.String, llproceso)
              }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}