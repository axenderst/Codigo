using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECI.Common
{
    public class Constantes
    {
        #region ctestatus
        public const int ID_ESTATUS_PROCESADO = 1;
        public const int ID_ESTATUS_CARGADO = 2;
        public const int ID_ESTATUS_ERROR = 3;
        public const int ID_ESTATUS_INICIADO = 4;
        #endregion

        public const string MESAJE_ERROR_HEADERS_MAPA = "Archivo(\"[HEADER_AR]\") > Mapa(\"[HEADER_MAP]\")";
    }
}
