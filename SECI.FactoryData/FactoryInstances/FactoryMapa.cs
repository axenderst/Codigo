using System;
using SECI.Entities;
using System.Data;


namespace SECI.FactoryData.FactoryInstances
{
    internal class FactoryMapa : BaseMethod<FactoryMapa, Mapas>

    {
        protected override Mapas _GetEntity(IDataReader dr)
        {
            return new Mapas()
            {
                mapaId = Convert.ToInt32(dr["IdMapa"]),
                ClaveMapa = Convert.ToString(dr["ClaveMapa"]),
                hoja = Convert.ToInt32(dr["Hoja"]),
                filaEncabezado = Convert.ToInt32(dr["FilaEncabezado"]),
                fechaCreacion = Convert.ToString(dr["FechaCreacion"]),
                fechaUltimaModif = Convert.ToString(dr["Fecha_UltimaModif"])
            };
        }
    }


    internal class FactoryMapaCompleto : BaseMethod<FactoryMapaCompleto, Mapas>
    {
        protected override Mapas _GetEntity(IDataReader dr)
        {
            return new Mapas()
            {

                mapaId = Convert.ToInt32(dr["IdMapa"]),
                ClaveMapa = Convert.ToString(dr["ClaveMapa"]),
                hoja = Convert.ToInt32(dr["Hoja"]),
                filaEncabezado = Convert.ToInt32(dr["FilaEncabezado"]),
                colMayorista = Convert.ToInt32(dr["Col_Mayorista"]),
                colPresentacion = Convert.ToInt32(dr["Col_Presentacion"]),
                colFecha = Convert.ToInt32(dr["Col_Fecha"]),
                colUnidades = Convert.ToInt32(dr["Col_Unidades"]),
                colMedico = Convert.ToInt32(dr["Col_Medico"]),
                colEstado = Convert.ToInt32(dr["Col_Estado"]),
                colHospital = Convert.ToInt32(dr["Col_Hospital"]),
                colSucursal = Convert.ToInt32(dr["Col_Sucursal"]),
                colLaboratorio = Convert.ToInt32(dr["Col_Laboratorio"]),
                colCiudad = Convert.ToInt32(dr["Col_Ciudad"]),
                colColonia = Convert.ToInt32(dr["Col_Colonia"]),
                colDireccion = Convert.ToInt32(dr["Col_Direccion"]),
                colCP = Convert.ToInt32(dr["Col_CP"]),
                colBrick = Convert.ToInt32(dr["Col_Brick"]),
                fechaCreacion = Convert.ToString(dr["FechaCreacion"]),
                fechaUltimaModif = Convert.ToString(dr["Fecha_UltimaModif"]),
                dsarchivo = Convert.ToString(dr["dsarchivo"])
            };
        }
    }


    internal class FactoryEncabezadoMapa : BaseMethod<FactoryEncabezadoMapa, EncabezadoMapas>
    {
        protected override EncabezadoMapas _GetEntity(IDataReader dr)
        {
            return new EncabezadoMapas()
            {
                llmapa = Convert.ToInt32(dr["IdMapa"]),
                colMayorista = Convert.ToString(dr["Col_Mayorista"]),
                colPresentacion = Convert.ToString(dr["Col_Presentacion"]),
                colFecha = Convert.ToString(dr["Col_Fecha"]),
                colUnidades = Convert.ToString(dr["Col_Unidades"]),
                colMedico = Convert.ToString(dr["Col_Medico"]),
                colEstado = Convert.ToString(dr["Col_Estado"]),
                colHospital = Convert.ToString(dr["Col_Hospital"]),
                colSucursal = Convert.ToString(dr["Col_Sucursal"]),
                colLaboratorio = Convert.ToString(dr["Col_Laboratorio"]),
                colCiudad = Convert.ToString(dr["Col_Ciudad"]),
                colColonia = Convert.ToString(dr["Col_Colonia"]),
                colDireccion = Convert.ToString(dr["Col_Direccion"]),
                colCP = Convert.ToString(dr["Col_CP"]),
                colBrick = Convert.ToString(dr["Col_Brick"]),
            };
        }
    }
}
