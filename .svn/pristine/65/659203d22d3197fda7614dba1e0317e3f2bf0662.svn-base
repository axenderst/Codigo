using System;
using SECI.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace SECI.Common.FTP
{
    public class InteraccionFTP
    {
        public static List<string> obtenerListaArchivos(Ftp FTPConfig, string anio, string mes)
        {
            try
            {
                List<string> listaArchivos = new List<string>();
                FtpWebRequest request = connectFTP(FTPConfig, anio, mes);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());

                while (!streamReader.EndOfStream)
                {
                    listaArchivos.Add(streamReader.ReadLine());
                }
                return listaArchivos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static FtpWebRequest connectFTP(Ftp FTPConfig, string anio, string mes)
        {
            try
            {
                // Obtiene el objeto que se utiliza para comunicarse con el servidor.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://" + FTPConfig.Dshost + "//" + anio + "//" + mes);
                
                request.Credentials = new NetworkCredential(FTPConfig.Dsusuario, FTPConfig.Dscontrasenia);
                return request;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
