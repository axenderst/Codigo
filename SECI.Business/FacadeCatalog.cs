using SECI.Entities;
using SECI.FactoryData.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECI.Business
{
    public class FacadeCatalog
    {
        //Realiza inserciones en catalogo 
        public static int InsertaCatalogo(Catalogo Catalogo)
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
    }
}
