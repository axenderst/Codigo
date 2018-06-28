using SECI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECI.FactoryData.FactoryInstances
{
    internal class FactoryFTP : BaseMethod<FactoryFTP, Ftp>
    {
        protected override Ftp _GetEntity(IDataReader dr)
        {
            return new Ftp()
            {
                Dshost = dr["dshost"].ToString(),
                Dspuerto = dr["dspuerto"].ToString(),
                Dsusuario = dr["dsusuario"].ToString(),
                Dscontrasenia = dr["dscontrasenia"].ToString()

            };
        }
    }
}

