using Capa_Datos;
using Capa_Entidad;
using System.Collections.Generic;
using System.Data;

namespace Capa_Negocio
{
    
    public class CN_Empleado
    {
        private CD_Empleado _dal = new CD_Empleado();



        public DataTable ObtenerCargos() => _dal.ObtenerCargos();
        public DataTable ObtenerEmpleadosPaginados(int pagina, int tamanoPagina, out int totalPaginas) => _dal.ObtenerEmpleadosPaginados(pagina, tamanoPagina, out totalPaginas);
        public void AgregarEmpleado(CE_Empleado empleado) => _dal.InsertarEmpleado(empleado);
        public void EliminarEmpleado(int idEmpleado) => _dal.EliminarEmpleado(idEmpleado);
        public CE_Empleado ObtenerEmpleadoPorId(int idEmpleado) => _dal.ObtenerEmpleadoPorId(idEmpleado);
        public void ActualizarEmpleado(CE_Empleado empleado) => _dal.ActualizarEmpleado(empleado);

    }
}