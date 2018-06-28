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
    public class Repositorios_CatalogoTem : BaseRepository
    {
        public int InsertaCatalogoTem(Catalogo Catalogo)
        {
            try
            {
                DbParameter ldCatalogo = DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB,
                    "@IdCatalog", DbType.Int32, DBNull.Value, -1, ParameterDirection.Output);
                using (TransactionScope scope = new TransactionScope())
                {
                    base._ProviderDB.ExecuteNonQuery("spIns_Catalogo_Tem", new DbParameter[] {
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
            }catch(Exception ex)
            {
                throw ex;
            }
           
            }



        //Muestra los datos que se almacenaron en la tabla catalogo temporal realizando busqueda por idmapa y llproceso
        public List<Catalogo> muestraCatalogoTem(Catalogo muestra)
        {
            try
            {
                return FactoryCatalog.GetList((DbDataReader)base._ProviderDB.GetDataReader("spGet_MostrarCatTemp", new DbParameter[]
              {
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@Id_mapa", DbType.String,muestra.Id_mapa),
                      DataFactory.GetObjParameter(GlobalConfiguration.ProviderDB, "@llproceso", DbType.String, muestra.llproceso)
              }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}