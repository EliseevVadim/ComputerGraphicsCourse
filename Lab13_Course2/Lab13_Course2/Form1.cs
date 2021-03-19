using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab13_Course2
{
    public partial class Form1 : Form
    {
        const int NMAX = 500;
        const double BIG = 1.0e30;
        Graphics graphics; 
        Pen pen;
        int n; 
        int[] v; 
        double[] x; 
        double[] y;
        ComboBox tbComboBox1;
        public const int bh = 40; 
        public const int bw = 40;
        public static int iRows = 20, iColumns = 2;
        public TextBox[,] tbArray;
        public Form1()
        {
            InitializeComponent();
            v = new int[NMAX];
            x = new double[NMAX];
            y = new double[NMAX];
            graphics = pictureBox1.CreateGraphics();
            pen = new Pen(Brushes.Black, 2);
            tbComboBox1 = new ComboBox()
            {
                Location = new Point(61, 31),
                Width = 121,
                Height = 21
            };
            panel1.Controls.Add(tbComboBox1);
            for (int i = 0; i < NMAX; i++) 
            { 
                tbComboBox1.Items.Add(20+i); 
            }
            tbComboBox1.SelectedItem = 20;
            tbComboBox1.SelectedIndexChanged += new EventHandler(UpdateBoxes);
            CreateBoxes(iRows, iColumns);
        }
        private void UpdateBoxes(object sender, EventArgs e)
        {
            CreateBoxes(Convert.ToInt32(tbComboBox1.SelectedItem), 2);
            panel1.Update();
        }
        public void CreateBoxes(int rows, int columns)
        {
            tbArray = new TextBox[rows, columns]; 
            int y1 = 80;
            for (int i = 0; i < rows; i++)
            {
                int x1 = 90; y1 += bh - 10;
                for (int j = 0; j < columns; j++)
                {
                    tbArray[i, j] = new TextBox(); 
                    tbArray[i, j].Name = "TextBox" + i + j;
                    panel1.Controls.Add(tbArray[i, j]);
                    tbArray[i, j].SetBounds(x1, y1, bw, bh);
                    x1 += bw;
                }
                tbArray[i, 0].Text = x[i].ToString("R");
                tbArray[i, 1].Text = y[i].ToString("R");
            }
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
        private void Draw(double x1, double y1, double x2, double y2)
        {
            Point point1 = new Point(IX(x1), IY(y1)); Point point2 = new Point(IX(x2), IY(y2));
            graphics.DrawLine(pen, point1, point2);
        }
        private unsafe bool CounterClock(int h, int i, int j, double* pdist)
        {
            double xh = x[v[h]], xi = x[v[i]], xj = x[v[j]],
            yh = y[v[h]], yi = y[v[i]], yj = y[v[j]],
            x_hi, y_hi, x_hj, y_hj, Determ;
            x_hi = xi - xh; y_hi = yi - yh; x_hj = xj - xh; y_hj = yj - yh;
            *pdist = x_hj * x_hj + y_hj * y_hj;
            Determ = x_hi * y_hj - x_hj * y_hi;
            return (Determ > 1e-6);
        }
        private void DrawPolygon()
        {
            int i; double xold, yold;
            xold = x[n - 1]; yold = y[n - 1];
            for (i = 0; i < n; i++)
            {
                Draw(xold, yold, x[i], y[i]); xold = x[i]; yold = y[i];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Triangulate(false);
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool finish = true;
            while (finish)
            {
                try
                {
                    Triangulate(true);
                    finish = false;
                }
                catch
                {
                    pen.DashStyle = DashStyle.Solid;
                }
            }
            MessageBox.Show("c'est magnifique!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private unsafe void Triangulate(bool randomly)
        {
            graphics.Clear(Color.White);
            int i, h, j, m, k, imin = 0;
            double diag, min_diag;
            n = Convert.ToInt16(tbComboBox1.SelectedItem.ToString());
            if (n >= NMAX)
            {
                MessageBox.Show("Количество вершин слишком велико!", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            Random random = new Random();
            for (i = 0; i < n; i++)
            {
                if (randomly)
                {
                    tbArray[i, 0].Text = (random.Next(0, 9) + random.NextDouble()).ToString();
                    tbArray[i, 1].Text = (random.Next(0, 6) + random.NextDouble()).ToString();
                }
                x[i] = Convert.ToDouble(tbArray[i, 0].Text);
                y[i] = Convert.ToDouble(tbArray[i, 1].Text);
                v[i] = i;
            }
            m = n;
            DrawPolygon();
            float[] dashValues = { 5, 5, 5, 5 };
            pen.DashPattern = dashValues;
            while (m > 3)
            {
                min_diag = BIG;
                for (i = 0; i < m; i++)
                {
                    h = (i == 0 ? m - 1 : i - 1); j = (i == m - 1 ? 0 : i + 1);
                    if (CounterClock(h, i, j, &diag) && (diag < min_diag))
                    {
                        min_diag = diag; imin = i;
                    }
                }
                i = imin; h = (i == 0 ? m - 1 : i - 1);
                j = (i == m - 1 ? 0 : i + 1);
                if (min_diag == BIG)
                {
                    if (!randomly)
                    {
                        MessageBox.Show("Неправильное направление обхода!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
                    }
                    throw new Exception();
                }
                Draw(x[v[h]], y[v[h]], x[v[j]], y[v[j]]);
                m--;
                for (k = i; k < m; k++)
                {
                    v[k] = v[k + 1];
                }
            }
            pen.DashStyle = DashStyle.Solid;
        }
    }
}