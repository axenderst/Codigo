using SECI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECI.FactoryData.FactoryInstances
{
   internal class FactoryUpProcess : BaseMethod<FactoryUpProcess, Procesos>
    {

        protected override Procesos _GetEntity(IDataReader dr)
        {
            return new Procesos()
            {
                llprocesos = Convert.ToInt32(dr["llprocesos"]),
                dslog = Convert.ToString(dr["dslog"])

            };
        }
    }
}
