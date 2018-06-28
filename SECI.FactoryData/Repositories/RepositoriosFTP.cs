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
    public class RepositoriosFTP : BaseRepository
    {
        public Ftp ConsultaFTP()
        {
            try
            {
                return FactoryFTP.Get((DbDataReader)base._ProviderDB.GetDataReader("spGetConfigFTP", new DbParameter[]  { }));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
           
    }
}