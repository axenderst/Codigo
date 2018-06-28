using System;
using System.Collections.Generic;
using SECI.FactoryData.Repositories;
using SECI.Entities;

namespace SECI.Business
{
    public class FacadeCatalogoTem
    {


        //Realiza inserciones en catalogo temporal
        public static int InsertCatalogoTemp(Catalogo Catalogo)
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

    //muestra los datos almacenados en catalogo temporal dependiendo IdMapa y llproceso
    public static List<Catalogo> ObtenerCatalogoTem(Catalogo catalogo)
    {
        try
        {
            return new Repositorios_CatalogoTem().muestraCatalogoTem(catalogo);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}

}

