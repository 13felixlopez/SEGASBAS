using System;
using System.Collections.Generic;
using Capa_Datos;
using Capa_Entidad; 

namespace Capa_Negocio
{
    public class CN_Cargo
    {
        private CD_Cargo _cdCargo = new CD_Cargo();

        public string AgregarCargo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return "El nombre del cargo no puede estar vacío.";
            }
            CE_Cargo cargo = new CE_Cargo() { Nombre = nombre };
            return _cdCargo.InsertarCargo(cargo);
        }

        public string EditarCargo(int id, string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return "El nombre del cargo no puede estar vacío.";
            }
            CE_Cargo cargo = new CE_Cargo() { Id_cargo = id, Nombre = nombre };
            return _cdCargo.ActualizarCargo(cargo);
        }

        public string EliminarCargo(int id)
        {
            return _cdCargo.EliminarCargo(id);
        }

        public List<CE_Cargo> ListarCargosConPaginado(int pagina, int tamanoPagina, out int totalRegistros)
        {
            return _cdCargo.ObtenerCargosConPaginado(pagina, tamanoPagina, out totalRegistros);
        }

        public List<CE_Cargo> BuscarCargos(string terminoBusqueda)
        {
            return _cdCargo.BuscarCargos(terminoBusqueda);
        }
    }
}