using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7_Course2
{
    public partial class Form1 : Form
    {
        bool flag = false;
        Graphics graphics;
        Image image = Image.FromFile("pic.jpg");
        int current_for_body = 415;
        int high_point = 350;
        Bitmap bitmap;
        int offset = 5;
        public Form1()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
        }
        void Move()
        {
            flag = true;
            Graphics buffDC = Graphics.FromImage(bitmap);
            current_for_body -= offset;
            high_point -= offset;
            buffDC.DrawImage(image, 0, 0);
            buffDC.FillRectangle(Brushes.DarkGray, 850, current_for_body, 80, 220);
            buffDC.DrawRectangle(Pens.Black, 850, current_for_body, 80, 220);
            buffDC.FillPolygon(Brushes.Red, new Point[3] { new Point(850, current_for_body), new Point(930, current_for_body), new Point(890, high_point) });
            for (int i = 0; i < 3; i++)
            {
                buffDC.FillEllipse(Brushes.DarkBlue, 875, current_for_body + 50 * i, 30, 30);
            }
            buffDC.FillRectangle(Brushes.DarkOliveGreen, 877, current_for_body + 160, 30, 60);
            if (high_point <= 290)
            {
                buffDC.FillPolygon(Brushes.Orange, new Point[] { new Point(850, current_for_body + 220), new Point(930, current_for_body + 220), new Point(920, current_for_body + 250), new Point(890, current_for_body + 290), new Point(860, current_for_body + 250) });
            }
            graphics.DrawImage(bitmap, 0, 0);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (!flag)
            {
                graphics.DrawImage(image, 0, 0);
                graphics.FillRectangle(Brushes.DarkGray, 850, current_for_body, 80, 220);
                graphics.DrawRectangle(Pens.Black, 850, current_for_body, 80, 220);
                graphics.FillPolygon(Brushes.Red, new Point[3] { new Point(850, current_for_body), new Point(930, current_for_body), new Point(890, high_point) });
                for (int i = 0; i < 3; i++)
                {
                    graphics.FillEllipse(Brushes.DarkBlue, 875, current_for_body + 50 * i, 30, 30);
                }
                graphics.FillRectangle(Brushes.DarkOliveGreen, 877, current_for_body + 160, 30, 60);
            }
            else
            {
                graphics.DrawImage(bitmap, 0, 0);
            }
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Move();
            if (current_for_body + 290 < 0)
            {
                timer1.Stop();
                current_for_body = 420;
                high_point = 355;
                Move();                
            }
        }
    }
}