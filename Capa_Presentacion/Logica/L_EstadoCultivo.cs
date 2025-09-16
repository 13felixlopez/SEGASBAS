namespace Capa_Presentacion.Logica
{
    public class L_EstadoCultivo
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
