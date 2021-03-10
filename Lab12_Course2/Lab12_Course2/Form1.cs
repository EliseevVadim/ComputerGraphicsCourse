using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab12_Course2
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        double[] x = new double[40];
        double[] y = new double[40];
        double eps = 0.04;
        double X, Y, t, xA, xB, xC, xD, yA, yB, yC, yD, a0, a1, a2, a3, b0, b1, b2, b3;
        int first;
        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
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
        private void button1_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            double Xold = 0;
            double Yold = 0;
            Random random=new Random();
            for(int i=0; i<40; i++)
            {
                x[i] = random.Next(1, 10) + random.NextDouble();
                y[i] = random.Next(1, 7) + random.NextDouble();
            }
            for(int i=0; i<x.Length; i++)
            {
                X = x[i];
                Y = y[i];
                Draw(X - eps, Y - eps, X + eps, Y + eps);
                Draw(X + eps, Y - eps, X - eps, Y + eps);
            }
            first = 1;
            for(int i=1; i<x.Length-2; i++)
            {
                xA = x[i - 1]; 
                xB = x[i]; 
                xC = x[i + 1]; 
                xD = x[i + 2];
                yA = y[i - 1]; 
                yB = y[i]; 
                yC = y[i + 1]; 
                yD = y[i + 2];
                a3 = (-xA + 3 * (xB - xC) + xD) / 6.0;
                b3 = (-yA + 3 * (yB - yC) + yD) / 6.0;
                a2 = (xA - 2 * xB + xC) / 2.0;
                b2 = (yA - 2 * yB + yC) / 2.0;
                a1 = (xC - xA) / 2.0;
                b1 = (yC - yA) / 2.0;
                a0 = (xA + 4 * xB + xC) / 6.0;
                b0 = (yA + 4 * yB + yC) / 6.0;
                for(int j=0; j<=x.Length; j++)
                {
                    t = (double)j / (double)x.Length;
                    X = ((a3 * t + a2) * t + a1) * t + a0;
                    Y = ((b3 * t + b2) * t + b1) * t + b0;
                    if (first == 1)
                    {
                        first = 0;
                    }
                    else
                    {
                        Draw(Xold, Yold, X, Y);
                    }
                    Xold = X; 
                    Yold = Y;
                }
            }
        }
    }
}
