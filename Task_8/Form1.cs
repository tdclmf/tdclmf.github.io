using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_8
{
    public partial class Form1 : Form
    {
        private PuzzleLogic _game;
        private int _tileSize; // Размер одной фишки
        private int _timeLeft; // Оставшееся время в секундах
        private string _currentPlayer = "Гость"; // Игрок по умолчанию
        private int _gameDuration = 300; // Время игры по умолчанию

        private Timer _gameTimer; // Таймер для отсчета времени
        private bool _isPlaying = false; // Идет ли сейчас игра
        public Form1()
        {
            InitializeComponent();
            _game = new PuzzleLogic();
            _tileSize = pictureBoxGame.Width / 4; // Считаем размер ячейки (320 / 4 = 80)

            // Инициализация таймера через код (чтобы не добавлять вручную)
            _gameTimer = new Timer();
            _gameTimer.Interval = 1000; // 1000 мс = 1 секунда
            _gameTimer.Tick += GameTimer_Tick;

            lblPlayer.Text = $"Игрок: {_currentPlayer}";
        }

        private void pictureBoxGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Настройка шрифта и формата текста для цифр
            Font font = new Font("Arial", 24, FontStyle.Bold);
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Проходим по всему двумерному массиву 4х4
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    int number = _game.Grid[r, c];

                    if (number != 0) // 0 - пустая ячейка, ее не рисуем
                    {
                        // Считаем координаты квадратика (X и Y)
                        int x = c * _tileSize;
                        int y = r * _tileSize;

                        // Создаем прямоугольник фишки (чуть меньше ячейки для красивых зазоров)
                        Rectangle rect = new Rectangle(x + 2, y + 2, _tileSize - 4, _tileSize - 4);

                        // Рисуем саму фишку (заливка + рамка)
                        using (Brush brush = new SolidBrush(_game.TileColor))
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            g.FillRectangle(brush, rect);
                            g.DrawRectangle(pen, rect);
                        }

                        // Рисуем цифру внутри прямоугольника
                        g.DrawString(number.ToString(), font, Brushes.Black, rect, format);
                    }
                }
            }
        }

        private void pictureBoxGame_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_isPlaying) return; // Если игра не начата, клики не работают

            // Переводим координаты клика мыши (X, Y) в индексы массива (Row, Col)
            int clickedRow = e.Y / _tileSize;
            int clickedCol = e.X / _tileSize;

            // Пытаемся сделать ход
            if (_game.Move(clickedRow, clickedCol))
            {
                pictureBoxGame.Invalidate(); // Если ход успешен, перерисовываем картинку

                // Проверяем, не победил ли игрок
                if (_game.CheckWin())
                {
                    _gameTimer.Stop();
                    _isPlaying = false;
                    MessageBox.Show($"Поздравляем, {_currentPlayer}! Вы собрали головоломку!", "Победа!");
                    // В будущем здесь добавим сохранение результата в файл
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _game.Shuffle();

            _timeLeft = _gameDuration;
            UpdateTimerLabel();

            _isPlaying = true;
            _gameTimer.Start();

            pictureBoxGame.Invalidate();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            _timeLeft--;
            UpdateTimerLabel();

            if (_timeLeft <= 0)
            {
                _gameTimer.Stop();
                _isPlaying = false;
                MessageBox.Show("Время вышло! Вы проиграли.", "Конец игры");
            }
        }

        private void UpdateTimerLabel()
        {
            int minutes = _timeLeft / 60;
            int seconds = _timeLeft % 60;
            lblTime.Text = $"Осталось: {minutes:00}:{seconds:00}";
        }
    }
}
