using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab15_Course2
{
    public struct Point3D
    {
        public int X;
        public int Y;
        public int Z;
    }
    public partial class Form1 : Form
    {
        Graphics graphics;
        int height = 50;
        double v11, v12, v13, v21, v22, v23, v32, v33, v43;
        double rho = 250.0, theta = 320.0, phi = 45.0;
        double c1 = 5.0, c2 = 3.5;
        double screenDist = 5;
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            phi = trackBar2.Value;
            CalculateCoefficients(rho, theta, phi);
            DrawParallelipiped(height);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CalculateCoefficients(rho, theta, phi);
            DrawParallelipiped(height);
        }
        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            theta = trackBar1.Value;
            CalculateCoefficients(rho, theta, phi);
            DrawParallelipiped(height);
        }
        private int IX(double x)
        {
            double xx = x * (pictureBox1.Size.Width / 10.0) + 0.5;
            return (int)xx;
        }
        private int IY(double y)
        {
            double yy = pictureBox1.Size.Height - y * (pictureBox1.Size.Height / 7.0) + 0.5;
            return (int)yy;
        }
        private void DrawParallelipiped(double h)
        {
            DrawPerspectiveLine(2 * h, -h, -h, 2 * h, h, -h);
            DrawPerspectiveLine(2 * h, h, -h, -2 * h, h, -h);
            DrawPerspectiveLine(-2 * h, h, -h, -2 * h, h, h);
            DrawPerspectiveLine(-2 * h, h, h, -2 * h, -h, h);
            DrawPerspectiveLine(-2 * h, -h, h, 2 * h, -h, h);
            DrawPerspectiveLine(2 * h, -h, h, 2 * h, -h, -h);
            DrawPerspectiveLine(2 * h, h, -h, 2 * h, h, h);
            DrawPerspectiveLine(2 * h, h, h, -2 * h, h, h);
            DrawPerspectiveLine(2 * h, h, h, 2 * h, -h, h);
            DrawPerspectiveLine(2 * h, -h, -h, -2 * h, -h, -h);
            DrawPerspectiveLine(-2 * h, -h, -h, -2 * h, h, -h);
            DrawPerspectiveLine(-2 * h, -h, -h, -2 * h, -h, h); 
        }
        private void DrawPerspectiveLine(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            double X1 = 0, Y1 = 0, X2 = 0, Y2 = 0;
            Perspective(x1, y1, z1, ref X1, ref Y1);
            Perspective(x2, y2, z2, ref X2, ref Y2);
            Point point1 = new Point(IX(X1), IY(Y1));
            Point point2 = new Point(IX(X2), IY(Y2));
            graphics.DrawLine(Pens.Black, point1, point2);
        }
        private void CalculateCoefficients(double rho, double theta, double phi)
        {
            double th, ph, coeff, costh, sinth, cosph, sinph;
            coeff = Math.PI / 180;
            th = theta * coeff;
            ph = phi * coeff;
            costh = Math.Cos(th);
            sinth = Math.Sin(th);
            cosph = Math.Cos(ph);
            sinph = Math.Sin(ph);
            v11 = -sinth;
            v12 = -cosph * costh;
            v13 = -sinph * costh;
            v21 = costh;
            v22 = -cosph * sinth;
            v23 = -sinph * sinth;
            v32 = sinph;
            v33 = -cosph;
            v43 = rho;
        }
        private void Perspective(double x, double y, double z, ref double pX, ref double pY)
        {
            double xe, ye, ze;
            xe = v11 * x + v21 * y;
            ye = v12 * x + v22 * y + v32 * z;
            ze = v13 * x + v23 * y + v33 * z + v43;
            pX = screenDist * xe / ze + c1;
            pY = screenDist * ye / ze + c2;
        }
    }
}
