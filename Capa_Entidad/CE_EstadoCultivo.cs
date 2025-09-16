using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class CE_EstadoCultivo
    {
        public int Id_estado_cultivo { get; set; }
        public string Nombre { get; set; }

       
        public int Id_estadoCultivo
        {
            get { return Id_estado_cultivo; }
            set { Id_estado_cultivo = value; }
        }
    }
}
