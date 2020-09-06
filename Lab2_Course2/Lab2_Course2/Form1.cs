using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_Course2
{
    public partial class Form1 : Form
    {
        bool pixels = false;
        bool nothing = false;
        bool inches = false;
        bool millimeters = false;

        Graphics graphics;
        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
        }   
        // отрисовка при смещениях
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (pixels)
            {
                button1_Click(sender, e);
            }
            else if (millimeters)
            {
                button2_Click(sender, e);
            }
            else if (inches)
            {
                button3_Click(sender, e);
            }
            else if (nothing)
            {
                button4_Click(sender, e);
            }
        }
        // пиксели
        private void button1_Click(object sender, EventArgs e)
        {
            pixels = true;
            inches = false;
            millimeters = false;
            nothing = false;
            graphics.PageUnit = GraphicsUnit.Pixel;
            pictureBox1.BackColor = Color.FromName("Azure");
            pictureBox1.Refresh();
            Pen pen = new Pen(Color.Red, 1);           
            graphics.DrawRectangle(pen, 0,0, pictureBox1.Width-1, pictureBox1.Height-1);
            graphics.DrawLine(pen, (pictureBox1.Width-1)/2, 0, (pictureBox1.Width-1)/2, pictureBox1.Height-1);
            graphics.DrawLine(pen, 0, (pictureBox1.Height - 1) / 2, pictureBox1.Width - 1, (pictureBox1.Height - 1) / 2);
            Pen graphicsPen = new Pen(Color.FromArgb(0, 255, 0), 1);
            graphics.TranslateTransform((pictureBox1.Width - 1 ) / 2, (pictureBox1.Height-1 )/2);
            graphics.RotateTransform(180f);
            PointF[] points = new PointF[17];
            int pos = 0;
            for (int x=-8; x<=8; x++)
            {
                points[pos].X = x;
                points[pos].Y =-6*x*x+3*x;
                pos++;
            }
            graphics.DrawCurve(graphicsPen, points);
            graphics.ResetTransform();
        }
        // миллиметры
        private void button2_Click(object sender, EventArgs e)
        {
            pixels = false;
            inches = false;
            millimeters = true;
            nothing = false;
            graphics.PageUnit = GraphicsUnit.Millimeter;
            pictureBox1.BackColor = Color.FromKnownColor(KnownColor.ControlLightLight);
            pictureBox1.Refresh();
            Pen pen = new Pen(Color.Red, 1f);
            int MMWidth = Convert.ToInt16((pictureBox1.Width - 1) / graphics.DpiX * 25.4);
            int MMHeight = Convert.ToInt16((pictureBox1.Height - 1) / graphics.DpiY * 25.4);
            graphics.DrawRectangle(pen, 0, 0, MMWidth, MMHeight);
            graphics.DrawLine(pen, MMWidth / 2, 0, MMWidth / 2, MMHeight);
            graphics.DrawLine(pen, 0, MMHeight / 2, MMWidth, MMHeight / 2);
            Pen graphicsPen = new Pen(Color.FromArgb(0, 0, 255), 0.5f);
            graphics.TranslateTransform((MMWidth+0.71f) / 2, MMHeight / 2);
            graphics.RotateTransform(180f);
            PointF[] points = new PointF[17];
            int pos = 0;
            for (int x = -8; x <= 8; x++)
            {
                points[pos].X = x;
                points[pos].Y = -6 * x * x + 3 * x;
                pos++;
            }
            graphics.DrawCurve(graphicsPen, points);
            graphics.ResetTransform();
        }
        // дюймы
        private void button3_Click(object sender, EventArgs e)
        {
            pixels = false;
            inches = true;
            millimeters = false;
            nothing = false;
            pictureBox1.BackColor = Color.FromName("LightCyan");
            pictureBox1.Refresh();
            graphics.PageUnit = GraphicsUnit.Inch;
            Pen pen = new Pen(Color.Red, 0.05f);
            float IWidth = (pictureBox1.Width - 1) / graphics.DpiX;
            float IHeight = (pictureBox1.Height - 1) / graphics.DpiY;
            graphics.DrawRectangle(pen, 0, 0, IWidth, IHeight);
            graphics.DrawLine(pen, IWidth / 2, 0, IWidth / 2, IHeight);
            graphics.DrawLine(pen, 0, IHeight / 2, IWidth, IHeight / 2);
            Pen graphicsPen = new Pen(Color.FromArgb(255, 255, 0), 0.05f);
            graphics.TranslateTransform((IWidth+0.5f)/2, (IHeight+0.71f)/2);
            graphics.RotateTransform(180f);
            PointF[] points = new PointF[17];
            int pos = 0;
            for (int x = -8; x <= 8; x++)
            {
                points[pos].X = x;
                points[pos].Y = -6 * x * x + 3 * x;
                pos++;
            }
            graphics.DrawCurve(graphicsPen, points);
            graphics.ResetTransform();
        }
        // очистить
        private void button4_Click(object sender, EventArgs e)
        {
            pixels = false;
            inches = false;
            millimeters = false;
            nothing = true;
            graphics.Clear(Color.FromArgb(255, 255, 255));
        }
    }
}
