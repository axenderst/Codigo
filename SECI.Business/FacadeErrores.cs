using SECI.Entities;
using SECI.FactoryData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECI.Business
{
    public class FacadeErrores
    {
        public static List<ProcesoMapas> ConsultaErrores(int anio, int mes)
        {
            try
            {
                return new RepositorioErrores().ConsultaErrores(anio, mes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertaErrorMapa(int llproceso, string dsarchivo)
        {
            try
            {
                return new RepositorioErrores().InsertaErrorMapa(llproceso, dsarchivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
