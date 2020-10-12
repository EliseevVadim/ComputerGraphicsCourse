using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace Lab8_Course2
{
    public partial class Form1 : Form
    {
        bool game_flag = false;
        bool help_flag = false;
        bool shot = false;
        bool lefted = false;
        int lives = 5;
        int offset = 20;
        bool menu = true;
        bool rotated = false;
        bool left, right, up, down;
        bool enemy_up, enemy_down, enemy_right, enemy_left;
        bool coloumns_here = false;
        Graphics graphics;
        Bitmap menu_bitmap;
        Graphics menuBuff;
        Graphics gameBuff;
        Graphics helpBuff;
        Bitmap help_bitmap;
        Bitmap game_bitmap;
        Rectangle play_rect = new Rectangle(500, 470, 200, 70);
        Rectangle ufo_rect = new Rectangle(10, 10, 100, 50);
        Rectangle enemy_rect = new Rectangle(1050, 150, 100, 50);
        Rectangle[] rectangles = new Rectangle[6];
        Rectangle shot_rect = new Rectangle(110, 35, 50, 6);
        Rectangle help = new Rectangle(10, 610, 50, 50);
        Image plate;
        public Form1()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
        }
        bool PlatesIntersect(Rectangle obj, Rectangle target)
        {
            if (obj.IntersectsWith(target))
            {
                return true;
            }
            return false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            menu_bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            game_bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            help_bitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            gameBuff = Graphics.FromImage(game_bitmap);
            menuBuff = Graphics.FromImage(menu_bitmap);
            helpBuff = Graphics.FromImage(help_bitmap);
        }
        void DrawMenu()
        {
            Image image = Image.FromFile(@"картинки/меню1.jpg");
            menuBuff.DrawImage(image, 0, 0, this.Width, this.Height);
            image = Image.FromFile(@"картинки/кнопка4.png");
            menuBuff.DrawImage(image, play_rect);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            menuBuff.DrawString("Добро пожаловать!", new Font("Arial", 46, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 0, 1200, 80), stringFormat);
            image = Image.FromFile(@"картинки/справка.png");
            menuBuff.DrawImage(image, help);
            graphics.DrawImage(menu_bitmap, 0, 0);
            image.Dispose();
        }
        void HelpDrawing()
        {
            Image image = Image.FromFile(@"картинки/меню1.jpg");
            helpBuff.DrawImage(image, 0, 0, ClientRectangle.Width, ClientRectangle.Height);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            helpBuff.DrawString("Справка", new Font("Arial", 46, FontStyle.Bold), Brushes.Red, new Rectangle(0, 0, 1200, 80), stringFormat);
            helpBuff.DrawString("Esc - выход в меню", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 70, 1200, 80), stringFormat);
            helpBuff.DrawString("W - вверх", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 100, 1200, 80), stringFormat);
            helpBuff.DrawString("S - вниз", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 130, 1200, 80), stringFormat);
            helpBuff.DrawString("А - налево", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 160, 1200, 80), stringFormat);
            helpBuff.DrawString("D - направо", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 190, 1200, 80), stringFormat);
            helpBuff.DrawString("Q - разворот", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 220, 1200, 80), stringFormat);
            helpBuff.DrawString("Е - в исходное положение", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 250, 1200, 80), stringFormat);
            helpBuff.DrawString("L - развернуть оружие", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 280, 1200, 80), stringFormat);
            helpBuff.DrawString("R - оружие в исходное положение", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 310, 1200, 80), stringFormat);
            helpBuff.DrawString("Space - выстрел", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 340, 1200, 80), stringFormat);
            helpBuff.DrawString("P.S. Для эффекта пошаговости сражения не рекомендуется зажимать кнопки перемещения", new Font("Arial", 24, FontStyle.Bold), Brushes.Aqua, new Rectangle(0, 600, 1200, 80), stringFormat);
            graphics.DrawImage(help_bitmap,0,0);
            image.Dispose();
        }
        void MainDrawing()
        {
            if (DoubleEdgeIntersect(ufo_rect) || PlatesIntersect(ufo_rect, enemy_rect))
            {
                if (up)
                {
                    ufo_rect.X += 5;
                    ufo_rect.Y += 5;
                    MainDrawing();
                }
                else if (right)
                {
                    ufo_rect.Y += 5;
                    ufo_rect.X -= 5;
                    MainDrawing();
                }
                else if (left)
                {
                    ufo_rect.Y += 5;
                    ufo_rect.X += 5;
                    MainDrawing();
                }
                else if (down)
                {
                    ufo_rect.X -= 5;
                    ufo_rect.Y -= 5;
                    MainDrawing();
                }
            }
            else if (ColoumnIntersect(ufo_rect, rectangles) && EdgeIntersect(ufo_rect))
            {
                if (up)
                {
                    ufo_rect.X += 5;
                    ufo_rect.Y += 5;
                    MainDrawing();
                }
                else if (right)
                {
                    ufo_rect.Y += 5;
                    ufo_rect.X -= 5;
                    MainDrawing();
                }
                else if (left)
                {
                    ufo_rect.Y += 5;
                    ufo_rect.X += 5;
                    MainDrawing();
                }
                else if (down)
                {
                    ufo_rect.X -= 5;
                    ufo_rect.Y -= 5;
                    MainDrawing();
                }
            }
            else if (EdgeIntersect(ufo_rect))
            {
                if (up)
                {
                    ufo_rect.X -= 5;
                    ufo_rect.Y += 5;
                    MainDrawing();
                }
                else if (right)
                {
                    ufo_rect.Y -= 5;
                    ufo_rect.X -= 5;
                    MainDrawing();
                }
                else if (left)
                {
                    ufo_rect.Y -= 5;
                    ufo_rect.X += 5;
                    MainDrawing();
                }
                else if (down)
                {
                    ufo_rect.X += 5;
                    ufo_rect.Y -= 5;
                    MainDrawing();
                }
            }
            else if (ColoumnIntersect(ufo_rect, rectangles))
            {
                if (up)
                {
                    ufo_rect.X -= 5;
                    ufo_rect.Y += 5;
                    MainDrawing();
                }
                else if (right)
                {
                    ufo_rect.Y -= 5;
                    ufo_rect.X -= 5;
                    MainDrawing();
                }
                else if (left)
                {
                    ufo_rect.Y -= 5;
                    ufo_rect.X += 5;
                    MainDrawing();
                }
                else if (down)
                {
                    ufo_rect.X += 5;
                    ufo_rect.Y -= 5;
                    MainDrawing();
                }
            }
            if (DoubleEdgeIntersect(enemy_rect) || PlatesIntersect(ufo_rect, enemy_rect))
            {
                if (enemy_up)
                {
                    enemy_rect.X += 5;
                    enemy_rect.Y += 5;
                    MainDrawing();
                }
                else if (enemy_right)
                {
                    enemy_rect.Y += 5;
                    enemy_rect.X -= 5;
                    MainDrawing();
                }
                else if (enemy_left)
                {
                    enemy_rect.Y += 5;
                    enemy_rect.X += 5;
                    MainDrawing();
                }
                else if (enemy_down)
                {
                    enemy_rect.X -= 5;
                    enemy_rect.Y -= 5;
                    MainDrawing();
                }
            }
            else if (ColoumnIntersect(enemy_rect, rectangles) && EdgeIntersect(enemy_rect))
            {
                if (enemy_up)
                {
                    enemy_rect.X += 5;
                    enemy_rect.Y += 5;
                    MainDrawing();
                }
                else if (enemy_right)
                {
                    enemy_rect.Y += 5;
                    enemy_rect.X -= 5;
                    MainDrawing();
                }
                else if (enemy_left)
                {
                    enemy_rect.Y += 5;
                    enemy_rect.X += 5;
                    MainDrawing();
                }
                else if (enemy_down)
                {
                    enemy_rect.X -= 5;
                    enemy_rect.Y -= 5;
                    MainDrawing();
                }
            }
            else if (EdgeIntersect(enemy_rect))
            {
                if (enemy_up)
                {
                    enemy_rect.X -= 5;
                    enemy_rect.Y += 5;
                    MainDrawing();
                }
                else if (enemy_right)
                {
                    enemy_rect.Y -= 5;
                    enemy_rect.X -= 5;
                    MainDrawing();
                }
                else if (enemy_left)
                {
                    enemy_rect.Y -= 5;
                    enemy_rect.X += 5;
                    MainDrawing();
                }
                else if (enemy_down)
                {
                    enemy_rect.X += 5;
                    enemy_rect.Y -= 5;
                    MainDrawing();
                }
            }
            else if (ColoumnIntersect(enemy_rect, rectangles))
            {
                if (enemy_up)
                {
                    enemy_rect.X -= 5;
                    enemy_rect.Y += 5;
                    MainDrawing();
                }
                else if (enemy_right)
                {
                    enemy_rect.Y -= 5;
                    enemy_rect.X -= 5;
                    MainDrawing();
                }
                else if (enemy_left)
                {
                    enemy_rect.Y -= 5;
                    enemy_rect.X += 5;
                    MainDrawing();
                }
                else if (enemy_down)
                {
                    enemy_rect.X += 5;
                    enemy_rect.Y -= 5;
                    MainDrawing();
                }
            }
            else
            {
                Image image = Image.FromFile(@"картинки/облака.jpg");
                gameBuff.DrawImage(image, 0, 0);
                int res = 0;
                Random random = new Random();
                if (!coloumns_here)
                    for (int i = 0; i < 6; i++)
                    {
                        if (i % 2 == 0)
                        {
                            rectangles[i] = new Rectangle(200 + 150 * i, 0, 50, random.Next(300, ClientRectangle.Height - 200));
                            gameBuff.DrawRectangle(Pens.Black, rectangles[i]);
                            gameBuff.FillRectangle(Brushes.Green, rectangles[i]);
                        }
                        else
                        {
                            res = random.Next(300, ClientRectangle.Height - 100);
                            rectangles[i] = new Rectangle(200 + 150 * i, res, 50, 500);
                            gameBuff.DrawRectangle(Pens.Black, rectangles[i]);
                            gameBuff.FillRectangle(Brushes.Green, rectangles[i]);
                        }
                    }
                else
                {
                    for (int i = 0; i < rectangles.Length; i++)
                    {
                        gameBuff.DrawRectangle(Pens.Black, rectangles[i]);
                        gameBuff.FillRectangle(Brushes.Green, rectangles[i]);
                    }
                }
                coloumns_here = true;
                image = Image.FromFile(@"картинки/сердце.png");
                gameBuff.DrawImage(image, ClientRectangle.Width - 120, 1, 50, 50);
                gameBuff.DrawString(lives.ToString() + "/5", new Font("Arial", 20, FontStyle.Bold), Brushes.DarkRed, new Rectangle(ClientRectangle.Width - 70, 10, 50, 50));
                if (!rotated)
                {
                    plate = Image.FromFile(@"картинки/НЛО.png");
                }
                else
                {
                    plate = Image.FromFile(@"картинки/НЛО1.png");
                }
                gameBuff.DrawImage(plate, ufo_rect);
                image = Image.FromFile(@"картинки/enemy.png");
                gameBuff.DrawImage(image, enemy_rect);
                if (shot)
                {
                    gameBuff.DrawRectangle(Pens.OrangeRed, shot_rect);
                    gameBuff.FillRectangle(Brushes.Yellow, shot_rect);
                }
                graphics.DrawImage(game_bitmap, 0, 0);
                image.Dispose();
                plate.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        bool EdgeIntersect(Rectangle obj)
        {
            if (obj.Location.X < 0 || obj.Location.Y < 0 || obj.Location.X > ClientRectangle.Width || obj.Location.Y > ClientRectangle.Height - ufo_rect.Width)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool DoubleEdgeIntersect(Rectangle obj)
        {
            if ((obj.Location.X < 0 && obj.Location.Y < 0) || (obj.Location.X < 0 && obj.Location.Y > ClientRectangle.Height - obj.Height) || (obj.Location.X > ClientRectangle.Width && obj.Location.Y < 0) || (obj.Location.X > ClientRectangle.Width && obj.Location.Y > ClientRectangle.Height - ufo_rect.Height))
            {
                return true;
            }
            return false;
        }
        bool ColoumnIntersect(Rectangle obj, Rectangle[] distances)
        {
            if

            (obj.IntersectsWith(distances[0]) || obj.IntersectsWith(distances[1]) || obj.IntersectsWith(distances[2]) || obj.IntersectsWith(distances[3]) || obj.IntersectsWith(distances[4]) || obj.IntersectsWith(distances[5]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ColoumnIntersect(shot_rect, rectangles) || EdgeIntersect(shot_rect) || DoubleEdgeIntersect(shot_rect))
            {
                shot = false;
                MainDrawing();
                shot_rect.X = ufo_rect.X + ufo_rect.Width;
                shot_rect.Y = ufo_rect.Y + ufo_rect.Height / 2;
                timer1.Stop();

            }
            else if (shot_rect.IntersectsWith(enemy_rect))
            {
                if (lives > 1)
                {
                    shot = false;
                    lives--;
                    MainDrawing();
                    if (!lefted)
                    {
                        shot_rect.X = ufo_rect.X + ufo_rect.Width;
                        shot_rect.Y = ufo_rect.Y + ufo_rect.Height / 2;
                    }
                    else
                    {
                        shot_rect.X = ufo_rect.X;
                        shot_rect.Y = ufo_rect.Y + ufo_rect.Height / 2;
                    }
                    timer1.Stop();
                }
                else
                {
                    lives = 5;
                    if (!lefted)
                    {
                        shot_rect.X = ufo_rect.X + ufo_rect.Width;
                        shot_rect.Y = ufo_rect.Y + ufo_rect.Height / 2;
                    }
                    else
                    {
                        shot_rect.X = ufo_rect.X;
                        shot_rect.Y = ufo_rect.Y + ufo_rect.Height / 2;
                    }
                    ufo_rect.X = 10;
                    ufo_rect.Y = 10;
                    enemy_rect.X = 1050;
                    enemy_rect.Y = 150;
                    shot = false;
                    timer1.Stop();
                    MainDrawing();
                }
            }
            else
            {
                if (!lefted)
                    shot_rect.X += 100;
                else
                {
                    shot_rect.X -= 100;
                }
                MainDrawing();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!menu)
            {
                Random random = new Random();
                int move = random.Next(2, 4);
                switch (move)
                {
                    case 2:
                        enemy_rect.X -= offset;
                        enemy_left = true;
                        enemy_down = false;
                        enemy_up = false;
                        enemy_right = false;
                        MainDrawing();
                        break;
                    case 3:
                        enemy_rect.Y += offset;
                        enemy_left = false;
                        enemy_down = true;
                        enemy_up = false;
                        enemy_right = false;
                        MainDrawing();
                        break;
                    case 4:
                        enemy_rect.Y -= offset;
                        enemy_left = false;
                        enemy_down = false;
                        enemy_up = true;
                        enemy_right = false;
                        MainDrawing();
                        break;
                }
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (menu)
            {
                DrawMenu();
            }
            else if (help_flag)
            {
                HelpDrawing();
            }
            else if(game_flag)
            {
                MainDrawing();
            }
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (menu)
            {
                if (play_rect.Contains(e.X, e.Y))
                {
                    menu = false;
                    help_flag = false;
                    game_flag = true;
                    timer2.Enabled = true;
                    MainDrawing();
                }
                if(help.Contains(e.X, e.Y))
                {
                    menu = false;
                    help_flag = true;
                    game_flag = false;
                    
                    HelpDrawing();
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!menu)
            {
                switch (e.KeyCode)
                {
                    //выход в меню
                    case Keys.Escape:
                        ufo_rect.X = 10;
                        ufo_rect.Y = 10;
                        menu = true;
                        coloumns_here = false;
                        timer2.Enabled = false;
                        DrawMenu();
                        break;
                    //движение вверх
                    case Keys.W:
                        ufo_rect.Y -= offset;
                        if (!shot)
                            shot_rect.Y -= offset;
                        up = true;
                        down = false;
                        left = false;
                        right = false;
                        MainDrawing();
                        break;
                    // налево
                    case Keys.A:
                        ufo_rect.X -= offset;
                        if (!shot)
                            shot_rect.X -= offset;
                        up = false;
                        down = false;
                        left = true;
                        right = false;
                        MainDrawing();
                        break;
                    // вниз
                    case Keys.S:
                        ufo_rect.Y += offset;
                        if (!shot)
                            shot_rect.Y += offset;
                        up = false;
                        down = true;
                        left = false;
                        right = false;
                        MainDrawing();
                        break;
                    // направо
                    case Keys.D:
                        ufo_rect.X += offset;
                        if (!shot)
                            shot_rect.X += offset;
                        up = false;
                        down = false;
                        left = false;
                        right = true;
                        MainDrawing();
                        break;
                    // разворот
                    case Keys.Q:
                        ufo_rect.Width = 50;
                        ufo_rect.Height = 100;
                        rotated = true;
                        MainDrawing();
                        break;
                    // разворот пушки
                    case Keys.L:
                        shot_rect.X = ufo_rect.X;
                        shot_rect.Y = ufo_rect.Y + ufo_rect.Height / 2;
                        lefted = true;
                        break;
                    // разворот обратно
                    case Keys.R:
                        shot_rect.X = ufo_rect.X+ufo_rect.Width;
                        shot_rect.Y = ufo_rect.Y + ufo_rect.Height / 2;
                        lefted = false;
                        break;
                    // обратно
                    case Keys.E:
                        ufo_rect.Width = 100;
                        ufo_rect.Height = 50;
                        rotated = false;
                        MainDrawing();
                        break;
                    // выстрел
                    case Keys.Space:
                        timer1.Enabled = true;
                        shot = true;
                        MainDrawing();
                        break;
                }
            }
        }
    }
}
