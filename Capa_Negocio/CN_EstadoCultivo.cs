using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_EstadoCultivo
    {
        private CD_EstadoCultivo _cdEstadoCultivo = new CD_EstadoCultivo();

        public List<CE_EstadoCultivo> ObtenerTodosEstadosCultivo()
        {
            return _cdEstadoCultivo.ObtenerTodosEstadosCultivo();
        }
    }
}
