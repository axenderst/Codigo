using System;
using SECI.Entities;
using System.Data;

namespace SECI.FactoryData.FactoryInstances
{
    internal class FactoryProcesos : BaseMethod<FactoryProcesos, Procesos>
    {
        protected override Procesos _GetEntity(IDataReader dr)
        {
            return new Procesos()
            {
                llprocesos = Convert.ToInt32(dr["llprocesos"]),
                boprocesomanual = Convert.ToBoolean(dr["boprocesomanual"]),
                fcinicio = Convert.ToString(dr["fcinicio"]),
                dslog = Convert.ToString(dr["dslog"]),
                IdMapa = Convert.ToInt32(dr["IdMapa"])

            };
        }
    }


    internal class FactoryProcesoMapas : BaseMethod<FactoryProcesoMapas, ProcesoMapas>
    {
        protected override ProcesoMapas _GetEntity(IDataReader dr)
        {
            return new ProcesoMapas()
            {
                llproceso = Convert.ToInt32(dr["llproceso"]),
                boprocesomanual = Convert.ToBoolean(dr["boprocesomanual"]),
                fcinicio = Convert.ToString(dr["fcinicio"]),
                dslog = Convert.ToString(dr["dslog"]),
                llmapa = Convert.ToInt32(dr["IdMapa"]),
                dsmapa = Convert.ToString(dr["ClaveMapa"]),
                dslogMapa = Convert.ToString(dr["dslogMapa"]),
                llestatusMapa = Convert.ToInt32(dr["llestatus"]),
                dsarchivo = Convert.ToString(dr["dsarchivo"])

            };
        }
    }

    internal class FactoryProcesosMapa : BaseMethod<FactoryProcesosMapa, Procesos>
    {
        protected override Procesos _GetEntity(IDataReader dr)
        {
            return new Procesos()
            {
                llprocesos = Convert.ToInt32(dr["llproceso"]),
                IdMapa = Convert.ToInt32(dr["IdMapa"]),
                dslog = Convert.ToString(dr["logmapa"])
            };
        }
    }
}
