using SECI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECI.FactoryData.FactoryInstances
{
    internal class FactoryCatalog : BaseMethod<FactoryCatalog, Catalogo>
    {
        protected override Catalogo _GetEntity(IDataReader dr)
        {
            return new Catalogo()
            {
                IdCatalog = Convert.ToInt32(dr["IdCatalog"]),
                Mayorista = Convert.ToString(dr["Mayorista"]),
                Presentacion = Convert.ToString(dr["Presentacion"]),
                Fecha = Convert.ToString(dr["Fecha"]),
                Unidades = Convert.ToInt32(dr["Unidades"]),
                Medico = Convert.ToString(dr["Medico"]),
                Estado = Convert.ToString(dr["Estado"]),
                Hospital = Convert.ToString(dr["Hospital"]),
                Sucursal = Convert.ToString(dr["Sucursal"]),
                Laboratorio = Convert.ToString(dr["Laboratorio"]),
                Ciudad = Convert.ToString(dr["Ciudad"]),
                Colonia = Convert.ToString(dr["Colonia"]),
                Direccion = Convert.ToString(dr["Direccion"]),
                CP = Convert.ToString(dr["CP"]),
                Brick = Convert.ToString(dr["Brick"]),
                Id_mapa = Convert.ToInt32(dr["Id_mapa"]),
                llproceso = Convert.ToInt32(dr["llproceso"])


            };
        }
    }
}

