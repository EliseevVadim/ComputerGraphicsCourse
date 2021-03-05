using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Lab11_Course2
{
    public partial class Form1 : Form
    {
        struct PointData
        {
            public double xx;
            public double yy;
            public int ii;
        }
        PointData data;
        public Form1()
        {
            InitializeComponent();
        }
        void SMove(double x, double y, BinaryWriter writer)
        {
            data.xx = x;
            data.yy = y;
            data.ii = 0;
            writer.Write(data.xx);
            writer.Write(data.yy);
            writer.Write(data.ii);
        }
        void SDraw(double x, double y, BinaryWriter writer)
        {
            data.xx = x;
            data.yy = y;
            data.ii = 1;
            writer.Write(data.xx);
            writer.Write(data.yy);
            writer.Write(data.ii);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            string path = openFileDialog.FileName;
            FileInfo file = new FileInfo(path);
            BinaryWriter writer = new BinaryWriter(file.Open(FileMode.Open, FileAccess.Write));
            int i; double r, alpha, phi0, phi, x0, y0, x1, y1, x2, y2;
            alpha = 60.0 * Math.PI / 180.0; phi0 = 0.0; x0 = 4.0; y0 = 4.0;
            SMove(x0, y0, writer);
            for (r = 0.5; r < 10.5; r += 0.5)
            {
                x2 = x0 + r * Math.Cos(phi0); y2 = y0 + r * Math.Sin(phi0);
                for (i = 1; i <= 6; i++)
                {
                    phi = phi0 + i * alpha;
                    x1 = x2; y1 = y2;
                    x2 = x0 + r * Math.Cos(phi); y2 = y0 + r * Math.Sin(phi);
                    SDraw(x1, y1, writer);
                    SDraw(x2, y2, writer);
                }
            }
            writer.Close();
            MessageBox.Show("Генерация произведена успешно", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
