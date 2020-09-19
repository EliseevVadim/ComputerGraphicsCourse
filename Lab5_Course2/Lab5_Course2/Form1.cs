using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Lab5_Course2
{
    public partial class Form1 : Form
    {
        bool clicked = false;
        Graphics graphics;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            clicked = true;
            pictureBox1.BackColor = Color.Azure;
            pictureBox1.Refresh();
            graphics = pictureBox1.CreateGraphics();
            graphics.DrawRectangle(new Pen(Color.Blue, 1), 0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
            graphics.DrawLine(new Pen(Color.Black, 1), 55, pictureBox1.Height - 25, pictureBox1.Width - 12, pictureBox1.Height - 25);
            graphics.DrawLine(new Pen(Color.Black, 1), 55, pictureBox1.Height - 25, 55, pictureBox1.Height - 475); // по 50 
            graphics.DrawString("0", new Font("Arial", 10f), Brushes.Black, 45, pictureBox1.Height - 35);
            Pen pen = new Pen(Color.DarkBlue, 1);
            pen.DashStyle = DashStyle.Dash;
            for (int i = 1; i <= 9; i++)
            {
                graphics.DrawLine(pen, 55, pictureBox1.Height - 25-50*i, pictureBox1.Width - 12, pictureBox1.Height - 25 - 50 * i);
                graphics.DrawString(i.ToString(), new Font("Arial", 10f), Brushes.Black, 45, pictureBox1.Height - 35 - 50 * i);
            }
            SolidBrush solidBrush = new SolidBrush(Color.Blue);
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Blue);
            Image image = Image.FromFile(@"D:\КГ\Lab5_Course2\Lab5_Course2\depositphotos_82062462-stock-photo-contrasting-texture.jpg");
            TextureBrush textureBrush = new TextureBrush(image);
            graphics.FillRectangle(solidBrush, new Rectangle(155, pictureBox1.Height - 125, 75, 100));
            graphics.DrawString("A", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, 185, pictureBox1.Height - 25);
            graphics.FillRectangle(hatchBrush, new Rectangle(325, pictureBox1.Height - 75, 75, 50));
            graphics.DrawString("B", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, 353, pictureBox1.Height - 25);
            graphics.FillRectangle(textureBrush, new Rectangle(500, pictureBox1.Height - 275, 75, 250));
            graphics.DrawString("E", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, 525, pictureBox1.Height - 25);
            graphics.FillRectangle(textureBrush, new Rectangle(675, pictureBox1.Height - 75, 75, 50));
            graphics.DrawString("FX", new Font("Arial", 16, FontStyle.Bold), Brushes.Black, 695, pictureBox1.Height - 25);
            graphics.DrawPolygon(new Pen(Color.Blue, 3), new PointF[] { new PointF(120,20), new PointF(180,30), new PointF(240, 20), new PointF(300, 30), new PointF(360,20), new PointF(600, 30), new PointF(600, 80), new PointF(360,90), new PointF(300, 80), new PointF(240, 90), new PointF(180, 80) });
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            graphics.DrawString("Оценки по программированию девяти студентов группы.", new Font("Arial", 10, FontStyle.Bold), Brushes.Black, 175, 45);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (clicked)
            {
                button1_Click(sender, e);
            }
        }
    }
}