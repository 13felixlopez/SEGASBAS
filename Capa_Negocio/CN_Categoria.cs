using System.Collections.Generic;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Categoria
    {
        private CD_Categoria objCapaDato = new CD_Categoria();

        public List<CE_Categoria> ListarPaginado(int paginaActual, int tamanoPagina, out int totalRegistros)
        {
            return objCapaDato.ListarPaginado(paginaActual, tamanoPagina, out totalRegistros);
        }

        public int Insertar(CE_Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.nombre == "")
            {
                Mensaje = "El nombre de la categoría no puede ser vacío";
                return 0;
            }
            return objCapaDato.Insertar(obj, out Mensaje);
        }

        public bool Editar(CE_Categoria obj, out string Mensaje)
        {
            Mensaje = string.Empty;
            if (obj.nombre == "")
            {
                Mensaje = "El nombre de la categoría no puede ser vacío";
                return false;
            }
            return objCapaDato.Editar(obj, out Mensaje);
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

        public List<CE_Categoria> Buscar(string textoBusqueda)
        {
            return objCapaDato.Buscar(textoBusqueda);
        }
    }
}