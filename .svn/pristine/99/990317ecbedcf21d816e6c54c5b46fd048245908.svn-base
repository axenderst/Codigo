using SECI.Entities;
using SECI.FactoryData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECI.Business
{
    public class FacadeProceso
    {
        //Obtiene procesos existentes
        public static List<Procesos> MuestraMvProceso()
        {
            try
            {
                return new RepositorioConsultas().MuestraProceso();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Inserta nuevos procesos
        public static int InsertarMvProceso(Procesos proceso)
        {
            try
            {
                return new RepositorioConsultas().InsertaProcesomv(proceso);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Modifica datos de un proceso
        public static int modificaMvProceso(Procesos proceso)
        {
            try
            {
                return new RepositorioConsultas().UpProcesos(proceso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Obtiene procesos existentes
        public static int InsertaProcMapas(Procesos proceso)
        {
            try
            {
                return new RepositorioConsultas().InsertaProcesoMapas(proceso);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public static int InsertaProcMapas()
        //{
        //    try
        //    {
        //        return new RepositorioConsultas().InsertaProcesoMapas();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static List<ProcesoMapas> ConsultaProcesoByFecha(int anio, int mes)
        {
            try
            {
                return new RepositorioProcesos().ConsultaProcesoByFecha(anio, mes);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int ActualizaLogProcesoMapa(ProcesoMapas datosMapa)
        {
            try
            {
                return new RepositorioProcesos().ActualizaLogProcesoMapa(datosMapa);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Procesos> ConsultaProcesoMapaByIdproceso(int llproceso)
        {
            try
            {
                return new RepositorioProcesos().ConsultaProcesoMapaByIdproceso(llproceso);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<ProcesoMapas> ConsultaAllProcesoMapaByIdproceso(int llproceso)
        {
            try
            {
                return new RepositorioProcesos().ConsultaAllProcesoMapaByIdproceso(llproceso);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static Procesos ConsultaProcesoMapaLog(string dslog)
        //{
        //    try
        //    {
        //        return new RepositorioProcesos().ConProcesoMpLog(dslog);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
