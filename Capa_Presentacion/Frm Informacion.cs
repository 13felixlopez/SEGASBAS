using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class Frm_Informacion : Form
    {
        public Frm_Informacion()
        {
            InitializeComponent();
       
            this.MinimizeBox = false;

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;

            this.ControlBox = true;
            this.StartPosition = FormStartPosition.CenterParent;

        }

        private void Frm_Informacion_Load(object sender, EventArgs e)
        {

        }
    }
}
