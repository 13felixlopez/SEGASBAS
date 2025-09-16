using System;
using System.Collections.Generic;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class CN_Lote
    {
        private CD_Lote objCapaDato = new CD_Lote();

        public List<CE_Lote> ObtenerLotesPaginados(int pagina, int tamanoPagina, out int totalRegistros)
        {
            return objCapaDato.ObtenerLotesPaginados(pagina, tamanoPagina, out totalRegistros);
        }

        public bool InsertarLote(CE_Lote oLote, out string mensaje)
        {
            if (string.IsNullOrEmpty(oLote.NombreLote) || string.IsNullOrEmpty(oLote.Manzanaje))
            {
                mensaje = "El nombre y el manzanaje son obligatorios.";
                return false;
            }
            return objCapaDato.InsertarLote(oLote, out mensaje);
        }

        public bool ActualizarLote(CE_Lote oLote, out string mensaje)
        {
            if (oLote.IDLote == 0)
            {
                mensaje = "ID de lote no válido para la actualización.";
                return false;
            }
            return objCapaDato.ActualizarLote(oLote, out mensaje);
        }

        public bool EliminarLote(int idLote, out string mensaje)
        {
            if (idLote == 0)
            {
                mensaje = "ID de lote no válido para la eliminación.";
                return false;
            }
            return objCapaDato.EliminarLote(idLote, out mensaje);
        }
    }
}