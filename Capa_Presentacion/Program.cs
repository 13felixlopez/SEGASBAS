using System;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new FrmLogin());
            }
            catch (Exception ex)
            {
           
                MessageBox.Show("Excepción no controlada al iniciar la aplicación:\n\n" + ex.ToString(),
                                "Error crítico (DEBUG)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                try
                {
                    System.IO.File.WriteAllText("error_debug.txt", ex.ToString());
                }
                catch { }
            }
        }
    }
}
