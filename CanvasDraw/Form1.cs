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
            RcDraw.Width = Math.Abs(e.X - RcDraw.X);
            RcDraw.Height = Math.Abs(e.Y - RcDraw.Y);
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
    }
}
