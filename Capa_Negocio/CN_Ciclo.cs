using Capa_Datos;
using Capa_Entidad;
using System.Collections.Generic;

namespace Capa_Negocio
{
    public class CN_Ciclo
    {
        private CD_Ciclo _cdCiclo = new CD_Ciclo();

        // Agregar un nuevo ciclo
        public string AgregarCiclo(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                return "La descripción del ciclo no puede estar vacía.";

            CE_Ciclo ciclo = new CE_Ciclo() { Descripcion = descripcion };
            return _cdCiclo.InsertarCiclo(ciclo);
        }

        // Actualizar un ciclo existente
        public string ActualizarCiclo(int id, string descripcion)
        {
            if (id <= 0)
                return "No se ha seleccionado un ciclo válido.";

            if (string.IsNullOrWhiteSpace(descripcion))
                return "La descripción del ciclo no puede estar vacía.";

            CE_Ciclo ciclo = new CE_Ciclo() { Id_ciclo = id, Descripcion = descripcion };
            return _cdCiclo.ActualizarCiclo(ciclo);
        }

        // Eliminar un ciclo
        public string EliminarCiclo(int id)
        {
            if (id <= 0)
                return "No se ha seleccionado un ciclo válido.";

            return _cdCiclo.EliminarCiclo(id);
        }

        public List<CE_Ciclo> ObtenerTodosCiclos()
        {
            int total;
            return _cdCiclo.ObtenerCiclosConPaginado(1, int.MaxValue, out total);
        }
        public List<CE_Ciclo> ListarCiclosConPaginado(int pagina, int tamanoPagina, out int totalRegistros)
        {
            return _cdCiclo.ObtenerCiclosConPaginado(pagina, tamanoPagina, out totalRegistros);
        }

        // Buscar ciclos por término de búsqueda
        public List<CE_Ciclo> BuscarCiclos(string terminoBusqueda)
        {
            if (string.IsNullOrWhiteSpace(terminoBusqueda))
                return new List<CE_Ciclo>();

            return _cdCiclo.BuscarCiclos(terminoBusqueda);
        }
    }
}
