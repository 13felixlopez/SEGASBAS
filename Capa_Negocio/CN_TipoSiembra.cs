using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_TipoSiembra
    {
        private CD_TipoSiembra _cdTipoSiembra = new CD_TipoSiembra();

        public List<CE_TipoSiembra> ObtenerTodosTiposSiembra()
        {
            return _cdTipoSiembra.ObtenerTodosTiposSiembra();
        }
    }
}
