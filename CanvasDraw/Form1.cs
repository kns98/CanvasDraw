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
            this.ResizeEnd += new EventHandler(Form1_ResizeEnd);

            saveFileDialog1.Filter = "Image files (*.jpg, *.png, *.bmp) | *.jpg; *.png; *.bmp";

        }

        private Stack<(Pen, Rectangle)> objects = new Stack<(Pen, Rectangle)>();

        private Rectangle RcDraw = new Rectangle();
        private float PenWidth = 5;

        private void Form1_ResizeEnd(Object sender, EventArgs e)
        {
            this.Invalidate();
        }

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

                //swap variables
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
                " " + e.Y + "[" + RcDraw.Width + " " + RcDraw.Height + "]";
            this.Invalidate(new Region(RcDraw));
        }

        private void Form1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            //Keep a stash of objects for saving
            Pen pen = new Pen(Color.Blue, PenWidth);
            pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            Rectangle rect = new Rectangle(new Point(RcDraw.X, RcDraw.Y), new Size(RcDraw.Width, RcDraw.Height));
            objects.Push((pen, rect));

            e.Graphics.DrawRectangle(pen, rect);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SaveControlImage()
        {

            Control ctr = this;
            try
            {
                saveFileDialog1.ShowDialog();
                var imageOutputStream = saveFileDialog1.OpenFile();
                using (Image bmp = new Bitmap(ctr.Width, ctr.Height))
                {

                    var gg = Graphics.FromImage(bmp);

                    foreach (var _obj in this.objects)
                    {
                        gg.DrawRectangle(_obj.Item1, _obj.Item2);
                    }

                    bmp.Save(imageOutputStream, System.Drawing.Imaging.ImageFormat.Bmp);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveControlImage();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Click down, hold, click up to draw a rectangle.", "Canvas Draw");
        }
    }
}
