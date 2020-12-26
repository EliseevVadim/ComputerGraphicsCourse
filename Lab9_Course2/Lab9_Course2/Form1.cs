using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab9_Course2
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        PointF pivot = new PointF(5, 1);
        float angle = (float)(-6 * Math.PI / 180);
        PointF[] buff = new PointF[4];
        PointF[] current = { new PointF(6, 1), new PointF(6, 2.5f), new PointF(8, 2.5f), new PointF(8, 1) };
        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //graphics.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            M1();
        }

        private void M1()
        {
            float angle = 6f;
            graphics.DrawPolygon(Pens.Red, current);
            for (int i = 0; i < 30; i++)
            {
                graphics.RotateTransform(-angle);
                graphics.DrawPolygon(Pens.Red, current);
            }
        }
        private void M2()
        {
            for(int i=0; i<30; i++)
            {
                for(int j=0; j<current.Length; j++)
                {
                    float X = (float)((current[j].X - pivot.X) * Math.Cos(angle) - (current[j].Y - pivot.Y) * Math.Sin(angle) + pivot.X);
                    float Y = (float)((current[j].X - pivot.X) * Math.Sin(angle) + (current[j].Y - pivot.Y) * Math.Cos(angle) + pivot.Y);
                    current[j] = new PointF(X, Y);
                }
                graphics.DrawPolygon(Pens.Red, current);
            }
        }
    }
}