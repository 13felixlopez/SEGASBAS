using Capa_Presentacion.Logica;
using System.Data;

namespace Capa_Presentacion.Datos
{
    public class NUser
    {
        readonly DUsuarios objd = new DUsuarios();

        public DataTable Nusers(LUsuarios parametros)
        {
            return objd.D_Usuarios(parametros);
        }
    }
}
