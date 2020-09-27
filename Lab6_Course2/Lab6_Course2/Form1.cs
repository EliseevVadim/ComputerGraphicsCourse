using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6_Course2
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        int end_of_lines = 150;
        int current_for_lines = 85;
        int current_for_ellipses = 80;
        int current_for_pies = 30;
        int current_for_points12 = 45;
        int current_for_points34 = 13;
        int current_for_line = 30;
        int offset = 2;
        bool toggle = false;
        bool stop = false;
        Graphics graphics;
        Rectangle rectangle = new Rectangle(195, 30, 65, 65);
        public Form1()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
        }
        void Move()
        {
            Graphics bufDC = Graphics.FromImage(bitmap);
            current_for_lines += offset;
            current_for_pies += offset;
            current_for_ellipses += offset;
            end_of_lines += offset;
            current_for_points12 += offset;
            current_for_points34 += offset;
            current_for_line += offset;
            bufDC.FillRectangle(Brushes.ForestGreen, 0, 3 * ClientRectangle.Height / 4, ClientRectangle.Width, ClientRectangle.Height);
            bufDC.FillRectangle(Brushes.SkyBlue, 0, 0, ClientRectangle.Width, 3 * ClientRectangle.Height / 4);
            bufDC.FillEllipse(Brushes.Yellow, ClientRectangle.Width - 180, 10, 130, 130);
            bufDC.FillPie(Brushes.Gray, 150, current_for_pies, 150, 150, 180f, 180f);
            Pen bottom_pen = new Pen(Color.Gray, 10);
            bottom_pen.EndCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
            bufDC.DrawLine(bottom_pen, 156, current_for_lines, 156, end_of_lines);
            bufDC.DrawLine(bottom_pen, 200, current_for_lines, 200, end_of_lines);
            bufDC.DrawLine(bottom_pen, 251, current_for_lines, 251, end_of_lines);
            bufDC.DrawLine(bottom_pen, 295, current_for_lines, 295, end_of_lines);
            bufDC.FillEllipse(Brushes.Red, 160, current_for_ellipses, 20, 20);
            bufDC.FillEllipse(Brushes.Red, 195, current_for_ellipses, 20, 20);
            bufDC.FillEllipse(Brushes.Red, 235, current_for_ellipses, 20, 20);
            bufDC.FillEllipse(Brushes.Red, 270, current_for_ellipses, 20, 20);
            Point[] points = new Point[4] { new Point(182, current_for_points12), new Point(269, current_for_points12), new Point(260, current_for_points34), new Point(192, current_for_points34) };
            if (!toggle)
            {
                bufDC.FillPolygon(Brushes.OrangeRed, points);
            }
            else
            {
                bufDC.FillPolygon(Brushes.BlueViolet, points);
            }
            toggle = !toggle;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            bufDC.DrawString("Старт", new Font("Arial", 14, FontStyle.Bold), Brushes.Blue, new Rectangle(195, current_for_line, 65,65), stringFormat);
            graphics.DrawImage(bitmap, 0,0);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (!stop)
            {
                graphics.FillRectangle(Brushes.ForestGreen, 0, 3 * ClientRectangle.Height / 4, ClientRectangle.Width, ClientRectangle.Height);
                graphics.FillRectangle(Brushes.SkyBlue, 0, 0, ClientRectangle.Width, 3 * ClientRectangle.Height / 4);
                graphics.FillEllipse(Brushes.Yellow, ClientRectangle.Width - 180, 10, 130, 130);
                graphics.FillPie(Brushes.Gray, 150, 30, 150, 150, 180f, 180f);
                Pen bottom_pen = new Pen(Color.Gray, 10);
                bottom_pen.EndCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
                graphics.DrawLine(bottom_pen, 156, 105, 156, 150);
                graphics.DrawLine(bottom_pen, 200, 105, 200, 150);
                graphics.DrawLine(bottom_pen, 251, 105, 251, 150);
                graphics.DrawLine(bottom_pen, 295, 105, 295, 150);
                graphics.FillEllipse(Brushes.Red, 160, 80, 20, 20);
                graphics.FillEllipse(Brushes.Red, 195, 80, 20, 20);
                graphics.FillEllipse(Brushes.Red, 235, 80, 20, 20);
                graphics.FillEllipse(Brushes.Red, 270, 80, 20, 20);
                Point[] points = new Point[4] { new Point(182, 45), new Point(269, 45), new Point(260, 13), new Point(192, 13) };
                graphics.FillPolygon(Brushes.Black, points);
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                graphics.DrawString("Старт", new Font("Arial", 14, FontStyle.Bold), Brushes.Blue, rectangle, stringFormat);
            }
            else
            {
                graphics.DrawImage(bitmap, 0, 0);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (rectangle.Contains(new Point(e.X, e.Y)))
            {
                for (int i=0; i<200; i++)
                {
                    Move();
                }
            }
            stop = true;
        }
    }
}