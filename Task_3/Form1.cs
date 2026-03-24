using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_3
{
    public partial class Form1 : Form
    {
        public enum MoveDirection { Horizontal, Vertical };
        public enum MoveStatus { Forward, Backward }; // Forward (вправо/вниз), Backward (влево/вверх)

        // Переменные состояния фигуры
        private int d = 30; // текущий диаметр (радиус * 2)
        private int maxD = 150; // предел увеличения фигуры
        private int x = 10, y = 50; // координаты
        private int step = 5; // шаг движения
        private MoveDirection currentDirection = MoveDirection.Horizontal;
        private MoveStatus currentStatus = MoveStatus.Forward;
        private Color colorForward = Color.Green;
        private Color colorBackward = Color.Orange;
        private SolidBrush brush;

        public Form1()
        {

            InitializeComponent();
            brush = new SolidBrush(colorForward);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void CenterBall()
        {
            x = (this.ClientSize.Width - d) / 2;
            y = (this.ClientSize.Height - d) / 2;
        }

        public int MySpeed
        {
            get { return 100 - timer1.Interval; }
            set
            {
                // Ограничиваем интервал от 1 до 99
                int newInterval = 100 - value;
                if (newInterval < 1) newInterval = 1;
                if (newInterval > 99) newInterval = 99;
                timer1.Interval = newInterval;
            }
        }

        public Color MyColorForward
        {
            get { return colorForward; }
            set { colorForward = value; UpdateBrushColor(); }
        }

        public Color MyColorBackward
        {
            get { return colorBackward; }
            set { colorBackward = value; UpdateBrushColor(); }
        }

        public MoveDirection MyDirection
        {
            get { return currentDirection; }
            set { currentDirection = value; }
        }


        private void UpdateBrushColor()
        {
            // Устанавливаем цвет в зависимости от текущего направления движения
            brush.Color = (currentStatus == MoveStatus.Forward) ? colorForward : colorBackward;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rectangle oldRc = new Rectangle(x - 3, y - 3, d + 6, d + 6);
            this.Invalidate(oldRc, true);

            if (currentDirection == MoveDirection.Horizontal)
            {
                if (currentStatus == MoveStatus.Forward) x += step;
                else x -= step;

                // Проверка столкновения с правым краем
                if (x >= (this.ClientSize.Width - d))
                {
                    x = this.ClientSize.Width - (d + 15);
                    HitEdge(MoveStatus.Backward);
                }
                // Проверка столкновения с левым краем
                else if (x <= 0)
                {
                    x = 0;
                    HitEdge(MoveStatus.Forward);
                }
            }
            else // Вертикальное движение
            {
                if (currentStatus == MoveStatus.Forward) y += step;
                else y -= step;

                // Проверка столкновения с нижним краем
                if (y >= (this.ClientSize.Height - d))
                {
                    y = this.ClientSize.Height - (d + 15);
                    HitEdge(MoveStatus.Backward);
                }
                // Проверка столкновения с верхним краем
                else if (y <= 0)
                {
                    y = 0;
                    HitEdge(MoveStatus.Forward);
                }
            }

            // Новую область тоже берем с запасом
            Rectangle newRc = new Rectangle(x - 3, y - 3, d + 6, d + 6);
            this.Invalidate(newRc, true);
        }

        private void HitEdge(MoveStatus newStatus)
        {
            currentStatus = newStatus; // Меняем направление
            d += 15; // Увеличиваем размер (радиус)
            UpdateBrushColor(); // Меняем цвет

            // Ограничение по радиусу (окончание работы)
            if (d >= maxD)
            {
                timer1.Stop();
                button1.Text = "Старт";
                MessageBox.Show("Достигнут максимальный размер фигуры. Работа завершена!", "Стоп");
                d = 30; // Сброс размера для нового запуска
                CenterBall();
                this.Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Сглаживание
            e.Graphics.FillEllipse(brush, x, y, d, d);
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                button1.Text = "Старт";
            }
            else
            {
                timer1.Start();
                button1.Text = "Стоп";
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Form2 frmSettings = new Form2();
            frmSettings.Owner = this; 
            frmSettings.ShowDialog(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterBall();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close(); 
            }
        }
    }
}