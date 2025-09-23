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
    public partial class FrmMapa : Form
    {
        private float currentZoom = 1.0f;
        private PointF lastMousePosition;
        private PointF imageOffset = new PointF(0, 0);
        public FrmMapa()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Dock = DockStyle.Fill;



            pictureBox1.MouseWheel += pictureBox1_MouseWheel;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.Paint += pictureBox1_Paint;
        }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            float zoomFactor = 1.2f;


            PointF mousePoint = e.Location;
            PointF newImageOffset = new PointF(
                imageOffset.X + mousePoint.X / currentZoom,
                imageOffset.Y + mousePoint.Y / currentZoom
            );

            if (e.Delta > 0)
            {
                currentZoom *= zoomFactor;
                newImageOffset = new PointF(
                    newImageOffset.X - mousePoint.X / currentZoom,
                    newImageOffset.Y - mousePoint.Y / currentZoom
                );
            }
            else
            {
                currentZoom /= zoomFactor;
                newImageOffset = new PointF(
                    newImageOffset.X - mousePoint.X / currentZoom,
                    newImageOffset.Y - mousePoint.Y / currentZoom
                );
            }

          
            currentZoom = Math.Max(0.5f, Math.Min(currentZoom, 5.0f));

           
            imageOffset = newImageOffset;
            pictureBox1.Invalidate();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                lastMousePosition = e.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
            
                float dx = e.X - lastMousePosition.X;
                float dy = e.Y - lastMousePosition.Y;

                imageOffset.X += dx / currentZoom;
                imageOffset.Y += dy / currentZoom;

               
                pictureBox1.Invalidate();

          
                lastMousePosition = e.Location;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image == null) return;

            // Dibuja la imagen del mapa con el nivel de zoom y el offset correctos
            e.Graphics.ScaleTransform(currentZoom, currentZoom);
            e.Graphics.DrawImage(pictureBox1.Image, imageOffset.X, imageOffset.Y, pictureBox1.Image.Width, pictureBox1.Image.Height);
        }
    }
}
