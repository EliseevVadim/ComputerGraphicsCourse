using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GenPlot
{
    public partial class Form1 : Form
    {
        bool flag;
        struct PointsData
        {
            public double xx;
            public double yy;
            public int ii;
        }
        PointsData data;
        double x, y, xmin, xmax, ymin, ymax;
        double X, Y, Xmin, Xmax, Ymin, Ymax;

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            try
            {
                string path = openFileDialog.FileName;
                double Xold = 0, Yold = 0;
                FileInfo file = new FileInfo(path);
                BinaryReader reader = new BinaryReader(file.Open(FileMode.Open, FileAccess.Read));
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    data.xx = reader.ReadDouble();
                    data.yy = reader.ReadDouble();
                    data.ii = reader.ReadInt32();
                    x = data.xx;
                    y = data.yy;
                    if (x < xmin)
                    {
                        xmin = x;
                    }
                    if (x > xmax)
                    {
                        xmax = x;
                    }
                    if (y < ymin)
                    {
                        ymin = y;
                    }
                    if (y > ymax)
                    {
                        ymax = y;
                    }
                }
                reader.Close();
                InitViewPort(Xmin, Ymin, Xmax, Ymax);
                fx = (Xmax - Xmin) / (xmax - xmin);
                fy = (Ymax - Ymin) / (ymax - ymin);
                f = Math.Min(fx, fy);
                xC = 0.5 * (xmin + xmax);
                yC = 0.5 * (ymin + ymax);
                XC = 0.5 * (Xmin + Xmax);
                YC = 0.5 * (Ymin + Ymax);
                c1 = XC - f * xC;
                c2 = YC - f * yC;
                reader = new BinaryReader(file.Open(FileMode.Open, FileAccess.Read));
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    data.xx = reader.ReadDouble();
                    data.yy = reader.ReadDouble();
                    data.ii = reader.ReadInt32();
                    x = data.xx;
                    y = data.yy;
                    X = f * x + c1;
                    Y = f * y + c2;
                    if (data.ii == 1)
                    {
                        
                        if (reader.BaseStream.Position != 40&&(reader.BaseStream.Position-40)%240!=0)
                        {
                            Draw(Xold, Yold, X, Y);
                        }
                        Xold = X;
                        Yold = Y;
                    }
                }
                reader.Close();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Xmin = double.Parse(textBox1.Text);
            Xmax = double.Parse(textBox2.Text);
            Ymin = double.Parse(textBox3.Text);
            Ymax = double.Parse(textBox4.Text);
        }

        double fx, fy, f, xC, yC, XC, YC, c1, c2;
        Graphics graphics;
        public Form1()
        {
            InitializeComponent();
            flag = false;
            graphics = pictureBox1.CreateGraphics();
            Xmin = 0.2; Xmax = 8.2; Ymin = 0.5; Ymax = 6.5;
            textBox1.Text = Xmin.ToString();
            textBox2.Text = Xmax.ToString();
            textBox3.Text = Ymin.ToString();
            textBox4.Text = Ymax.ToString();
        }
        private int IX(double x)
        {
            double xx = x * (pictureBox1.Size.Width / 10.0) + 0.5;
            return (int)xx;
        }
        private int IY(double y)
        {
            double yy = pictureBox1.Size.Height - y *(pictureBox1.Size.Height / 7.0) + 0.5;
            return (int)yy;
        }
        private void Draw(double x1, double y1, double x2, double y2)
        {
            Point point1 = new Point(IX(x1), IY(y1));
            Point point2 = new Point(IX(x2), IY(y2));
            graphics.DrawLine(Pens.Black, point1, point2);
        }
        private void InitViewPort(double Xmin, double Ymin, double Xmax, double Ymax)
        {
            Draw(Xmin, Ymin, Xmin + 0.2, Ymin);
            Draw(Xmin, Ymin, Xmin, Ymin + 0.2);
            Draw(Xmin, Ymax, Xmin + 0.2, Ymax);
            Draw(Xmin, Ymax, Xmin, Ymax - 0.2);
            Draw(Xmax, Ymin, Xmax - 0.2, Ymin);
            Draw(Xmax, Ymin, Xmax, Ymin + 0.2);
            Draw(Xmax, Ymax, Xmax - 0.2, Ymax);
            Draw(Xmax, Ymax, Xmax, Ymax - 0.2);
            Draw(Xmax / 2 - 0.2, Ymin, Xmax / 2 + 0.2, Ymin);
            Draw(Xmax / 2, Ymin, Xmax / 2, Ymin + 0.2);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
        }
    }
}
