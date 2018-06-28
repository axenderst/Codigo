using SECI.Entities;
using SECI.FactoryData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECI.Business
{
    public class FacadeCatalogo
    {
        //Realiza inserciones en catalogo 
        public static int InsertaCatalogo(Catalogo Catalogo)
        {
            try
            {
                return new RepositorioConsultas().InsertaCatalogoTem(Catalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Catalogo> ConsultaCatalogoByProcesoMapa(int llproceso, int llmapa)
        {
            try
            {
                return new RepositoriosCatalogo().ConsultaCatalogoByProcesoMapa(llproceso, llmapa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertaCatalogo2(Catalogo Catalogo)
        {
            try
            {
                return new RepositorioConsultas().InsertaCatalogo2(Catalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertaCatalogoMensual(List<Procesos> Catalogo)
        {
            try
            {
                return new RepositorioConsultas().InsertaCatalogoMensual(Catalogo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
