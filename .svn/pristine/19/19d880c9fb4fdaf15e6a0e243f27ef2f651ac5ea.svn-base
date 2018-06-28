using SECI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECI.FactoryData.FactoryInstances
{
    internal class FactoryNewCatalog : BaseMethod<FactoryNewCatalog,Procesos>
    {

        protected override Procesos _GetEntity(IDataReader dr)
        {
            return new Procesos()
            {
                llprocesos = Convert.ToInt32(dr["llprocesos"]),
                IdMapa = Convert.ToInt32(dr["IdMapa"])

            };
        }
    }
}
