using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4_Course2
{
    public partial class Form1 : Form
    {
        string dragon_code = "1101100111001001110110001100100111011001110010001101100011001001110110011100100111011000110010001101100111001000110110001100100"; // кривая дракона 7го порядка
        bool clicked = false;
        Graphics graphics;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            clicked = true;
            graphics = pictureBox1.CreateGraphics();
            int offset = 20;
            int start_x = pictureBox1.Width / 2;
            int start_y = pictureBox1.Height / 2;
            int next_x = pictureBox1.Width / 2;
            int next_y = pictureBox1.Height / 2 - offset;
            int end_x = next_x;
            int end_y = next_y;
            Pen head = new Pen(Color.Black, 10);
            Pen body = new Pen(Color.Black);
            head.CompoundArray = new float[] { 0, 0.2f, 0.3f, 0.6f, 0.7f, 1 };
            graphics.DrawLine(head, start_x, start_y, end_x, end_y);            
            body.DashStyle = DashStyle.Dash;
            for (int i = 0; i < dragon_code.Length; i++)
            {
                if (next_y - start_y < 0)// шли вверх
                {
                    end_y = next_y;
                    if (dragon_code[i] == '1')
                    {
                        end_x = next_x - offset;// <-
                    }
                    else
                    {
                        end_x = next_x + offset;// ->
                    }
                }
                if (next_y - start_y > 0)// шли вниз
                {
                    end_y = next_y;
                    if (dragon_code[i] == '1')
                    {
                        end_x = next_x + offset;// <-
                    }
                    else
                    {
                        end_x = next_x - offset;// ->
                    }
                }
                if (next_x - start_x < 0)// шли влево
                {
                    end_x = next_x;
                    if (dragon_code[i] == '1')
                    {
                        end_y = next_y + offset;// вниз
                    }
                    else
                    {
                        end_y = next_y - offset;// вверх
                    }
                }
                if (next_x - start_x > 0)// шли вправо
                {
                    end_x = next_x;
                    if (dragon_code[i] == '1')
                    {
                        end_y = next_y - offset;// вверх
                    }
                    else
                    {
                        end_y = next_y + offset;// вниз
                    }
                }
                graphics.DrawLine(body, next_x, next_y, end_x, end_y);
                start_x = next_x;
                start_y = next_y;
                next_x = end_x;
                next_y = end_y;
            }
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