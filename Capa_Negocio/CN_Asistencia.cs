using System.Collections.Generic;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Asistencia
    {
        private CD_Asistencia objAsistenciaData = new CD_Asistencia();

        public List<CE_Asistencia> ObtenerAsistenciasPaginadas(int pagina, int tamanoPagina, out int totalRegistros)
        {
            return objAsistenciaData.ObtenerAsistenciasPaginadas(pagina, tamanoPagina, out totalRegistros);
        }

        public bool InsertarAsistencia(CE_Asistencia obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.IDEmpleado == 0)
            {
                Mensaje = "Debe seleccionar un empleado.";
                return false;
            }
            return objAsistenciaData.InsertarAsistencia(obj, out Mensaje);
        }

        public bool EditarAsistencia(CE_Asistencia obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.IDEmpleado == 0)
            {
                Mensaje = "Debe seleccionar un empleado.";
                return false;
            }
            return objAsistenciaData.EditarAsistencia(obj, out Mensaje);
        }

        public bool EliminarAsistencia(int idAsistencia, out string Mensaje)
        {
            return objAsistenciaData.EliminarAsistencia(idAsistencia, out Mensaje);
        }

        public List<CE_Asistencia> BuscarAsistencias(string terminoBusqueda)
        {
            return objAsistenciaData.BuscarAsistencias(terminoBusqueda);
        }
    }
}   