using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CanvasDraw
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Paint += new PaintEventHandler(this.Form1_Paint);

        }

        private Rectangle RcDraw = new Rectangle();
        private float PenWidth = 5;

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RcDraw.X = e.X;
            RcDraw.Y = e.Y;
            label1.Text = "Down : " + DateTime.Now.ToString() + " " + e.X + " " + e.Y;
        }

        private void Form1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X < RcDraw.X || e.Y < RcDraw.Y)
            {
                var tmp_ex = e.X;
                var tmp_ey = e.Y;

                (tmp_ex, RcDraw.X) = (RcDraw.X, tmp_ex);
                (tmp_ey, RcDraw.Y) = (RcDraw.Y, tmp_ey);

                RcDraw.Width = Math.Abs(tmp_ex - RcDraw.X);
                RcDraw.Height = Math.Abs(tmp_ey - RcDraw.Y);
            }
            else
            {
                RcDraw.Width = Math.Abs(e.X - RcDraw.X);
                RcDraw.Height = Math.Abs(e.Y - RcDraw.Y);
            }

            label2.Text = "Up : " + DateTime.Now.ToString() + " " + e.X + 
                " " + e.Y + "["+ RcDraw.Width + " " + RcDraw.Height + "]";
            this.Invalidate(new Region(RcDraw));
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Draw the rectangle...
            e.Graphics.DrawRectangle(new Pen(Color.Blue, PenWidth), RcDraw);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SaveControlImage(Control ctr)
        {
            try
            {
                var imagePath = @"C:\Image.png";
                Image bmp = new Bitmap(ctr.Width, ctr.Height);
                var gg = Graphics.FromImage(bmp);
                var rect = ctr.RectangleToScreen(ctr.ClientRectangle);
                gg.CopyFromScreen(rect.Location, Point.Empty, ctr.Size);
                bmp.Save(imagePath);
            }
            catch (Exception)
            {
                //
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SaveControlImage();
        }
    }
}
