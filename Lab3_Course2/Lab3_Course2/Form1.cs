using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Lab3_Course2
{
    public partial class Form1 : Form
    {
        bool writen = false;
        Graphics graphics;
        string filename = Environment.CurrentDirectory + @"\строки.txt";
        string[] lines = { "First line", "Second line", "Third line", "Fourth line", "Fifth line", "Sixth line", "Seventh line", "Eighth line", "Ninth line", "Tenth line" };
        public Form1()
        {
            InitializeComponent();
            graphics = pictureBox1.CreateGraphics();
        }
        // запись в файл
        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(filename))
            {
                StreamWriter writer = File.CreateText(filename);
                foreach (string s in lines)
                {
                    writer.WriteLine(s);
                }
                MessageBox.Show("Текстовый файл успешно создан", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                writer.Close();
            }
            else
            {
                MessageBox.Show("Текстовый файл уже создан", "Замечание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // отображение
        private void button2_Click(object sender, EventArgs e)
        {            
            try
            {
                Font font1 = new Font("Arial", 36, FontStyle.Underline);
                Font font2 = new Font("Broadway", 24, FontStyle.Regular);
                Font font3 = new Font("Times New Roman", 36, FontStyle.Strikeout);
                StringFormat stringFormat1 = (StringFormat)StringFormat.GenericTypographic.Clone();
                stringFormat1.FormatFlags = StringFormatFlags.DirectionVertical;
                stringFormat1.Alignment = StringAlignment.Center;
                stringFormat1.LineAlignment = StringAlignment.Center;
                StringFormat stringFormat2 = (StringFormat)StringFormat.GenericTypographic.Clone();
                stringFormat2.Alignment = StringAlignment.Far;
                stringFormat2.LineAlignment = StringAlignment.Near;
                StringFormat stringFormat3 = (StringFormat)StringFormat.GenericTypographic.Clone();
                stringFormat3.Alignment = StringAlignment.Near;
                stringFormat3.LineAlignment = StringAlignment.Far;
                string[] mas = File.ReadAllLines(filename);
                pictureBox1.BackColor = Color.Azure;
                pictureBox1.Refresh();
                for(int i=0; i<mas.Length; i++)
                {
                    if (i >= 0 && i < 6)
                    {                        
                        graphics.DrawString(mas[i], font1, Brushes.Blue, new RectangleF(380-i*45, -100, pictureBox1.Width-20, pictureBox1.Height-20), stringFormat1);                       
                    }
                    else if (i >= 6 && i < 9)
                    {
                        graphics.DrawString(mas[i], font2, Brushes.Black, new RectangleF(-700, i * 35-200, pictureBox1.Width - 20, pictureBox1.Height - 20), stringFormat2);
                    }
                    else
                    {
                        graphics.DrawString(mas[i], font3, Brushes.Green, new RectangleF(400, 0, pictureBox1.Width - 20, pictureBox1.Height - 20), stringFormat3);
                    }
                }
                writen = true;
            }
            catch
            {
                MessageBox.Show("Ошибка чтения файла. Проверьте его наличиие", "Замечание", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);                
            }
        }
        // очистка
        private void button3_Click(object sender, EventArgs e)
        {
            if (writen)
            {
                graphics.Clear(Color.FromKnownColor(KnownColor.ControlLightLight));
            }
            if (File.Exists(filename))
            {                
                writen = false;
                File.Delete(filename);
                MessageBox.Show("Текстовый файл успешно удален", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                writen = false;                
                MessageBox.Show("Текстовый файл уже удален либо не создан", "Замечание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
        }
        private void button1_Paint(object sender, PaintEventArgs e)
        {
            if (writen)
            {
                button2_Click(sender, e);
            }
        }
    }
}
