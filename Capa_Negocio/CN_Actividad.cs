using Capa_Datos;
using Capa_Entidad;
using System.Collections.Generic;

namespace Capa_Negocio
{
    public class CN_Actividad
    {
        private CD_Actividad _cdActividad = new CD_Actividad();

        public string AgregarActividad(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return "La descripción de la actividad no puede estar vacía.";
            }
            CE_Actividad actividad = new CE_Actividad() { Descripcion = descripcion };
            return _cdActividad.InsertarActividad(actividad);
        }

        public string EditarActividad(int id, string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return "La descripción de la actividad no puede estar vacía.";
            }
            CE_Actividad actividad = new CE_Actividad() { Id_actividad = id, Descripcion = descripcion };
            return _cdActividad.ActualizarActividad(actividad);
        }

        public string EliminarActividad(int id)
        {
            return _cdActividad.EliminarActividad(id);
        }

        public List<CE_Actividad> ListarActividadesConPaginado(int pagina, int tamanoPagina, out int totalRegistros)
        {
            return _cdActividad.ObtenerActividadesConPaginado(pagina, tamanoPagina, out totalRegistros);
        }

        public List<CE_Actividad> BuscarActividades(string terminoBusqueda)
        {
            return _cdActividad.BuscarActividades(terminoBusqueda);
        }
    }
}