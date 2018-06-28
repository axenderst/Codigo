using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace SECI.Security
{
    public class GlobalConfiguration
    {
        public static string KeyForCryptoStrings { get { return "Stygian"; } }

        #region Cadena de conexion SIC
        /// <summary>
        /// Obtiene la cadena de conexión
        /// </summary>
        public static string StringConectionDB
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    //return connectionString;
                    //return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        /// <summary>
        /// Obtiene el proveedor para la base de datos
        /// </summary>
        public static string ProviderDB
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["DefaultConnection"].ProviderName;

                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido el proveedor para la base de datos en el archivo de configuración.");
                }
            }
        }

        #endregion#region Cadena de conexion Dynamics MIRA
        // ================================================= Conexion a base de datos Views =======================================
        /// <summary>
        /// Obtiene la cadena de conexión
        /// </summary>
        public static string StringConectionDBViews
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["ViewsConnection"].ConnectionString;
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        /// <summary>
        /// Obtiene el proveedor para la base de datos
        /// </summary>
        public static string ProviderDBViews
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["ViewsConnection"].ProviderName;

                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido el proveedor para la base de datos en el archivo de configuración.");
                }
            }
        }


        #region Cadena de conexion Usuarios MIRA
        // ================================================= Conexion a base de datos Views =======================================
        /// <summary>
        /// Obtiene la cadena de conexión
        /// </summary>
        public static string StringConectionDBUsersMira
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["ViewsConnectionUsers"].ConnectionString;
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        /// <summary>
        /// Obtiene el proveedor para la base de datos
        /// </summary>
        public static string ProviderDBUsersMira
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["ViewsConnectionUsers"].ProviderName;

                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido el proveedor para la base de datos en el archivo de configuración.");
                }
            }
        }
        #endregion

        #region Cadena de conexion ReQlogic
        // ================================================= Conexion a base de datos Views =======================================
        /// <summary>
        /// Obtiene la cadena de conexión
        /// </summary>
        public static string StringConectionDBReQlogic
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["ReQlogicConnection"].ConnectionString;
                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido la cadena de conexión para la base de datos en el archivo de configuración.");
                }
            }
        }

        /// <summary>
        /// Obtiene el proveedor para la base de datos
        /// </summary>
        public static string ProviderDBReQlogic
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["ReQlogicConnection"].ProviderName;

                }
                catch (NullReferenceException)
                {
                    throw new Exception("No se ha definido el proveedor para la base de datos en el archivo de configuración.");
                }
            }
        }
        #endregion
    }
}

