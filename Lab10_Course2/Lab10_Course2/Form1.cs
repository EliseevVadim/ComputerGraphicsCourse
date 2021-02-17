using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab10_Course2
{

    public partial class Form1 : Form
    {
        Polygon pol;
        Graphics graphics;
        public Form1()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            pol = new Polygon((new PointF[4] { new PointF(-300, 0), new PointF(0, 150), new PointF(300, 0), new PointF(0, -150) }));
        }
        void DrawTargets()
        {
            List<Segment> segments = new List<Segment>();
            int i;
            double pi, alpha, phi0, phi, x0, y0, x1, y1, x2, y2;
            pi = Math.PI;
            alpha = 72.0 * pi / 180.0;
            phi0 = 0;
            x0 = 4;
            y0 = 4;
            for (double r = 20; r < 850; r += 10)
            {
                x2 = x0 + r * Math.Cos(phi0); y2 = y0 + r * Math.Sin(phi0);
                for (i = 1; i <= 5; i++)
                {
                    phi = phi0 + i * alpha;
                    x1 = x2; y1 = y2;
                    x2 = x0 + r * Math.Cos(phi); y2 = y0 + r * Math.Sin(phi);
                    Segment segment = new Segment(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2));
                    segments.Add(segment);
                }
                List<Segment> res = pol.CyrusBeckClip(segments);
                foreach (Segment s in res)
                {
                    graphics.DrawLine(Pens.Black, s.Start, s.End);
                }
                segments.Clear();
                res.Clear();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            graphics.TranslateTransform(this.Width / 2, this.Height / 2);
            graphics.DrawPolygon(Pens.Black, pol.ToArray());
            DrawTargets();
        }
    }
}