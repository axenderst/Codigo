using System;
using System.Collections.Generic;
using SECI.FactoryData.Repositories;
using SECI.Entities;

namespace SECI.Business
{
    public class FacadeFTP
    {

        /// <summary>
        /// Consulta los datos para conectar al FTP
        /// </summary>
        /// <returns></returns>
        public static Ftp ConsultaFTP()
        {
            try
            {
                return new RepositoriosFTP().ConsultaFTP();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}

