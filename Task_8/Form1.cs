using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_8
{
    public partial class Form1 : Form
    {
        private PuzzleLogic _game;
        private string _recordsFile = "records.dat";
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
            _tileSize = pictureBoxGame.Width / 4; // Считаем размер ячейки
            // Инициализация таймера
            _gameTimer = new Timer();
            _gameTimer.Interval = 1000; // 1000 мс = 1 секунда
            _gameTimer.Tick += GameTimer_Tick;

            lblPlayer.Text = $"Игрок: {_currentPlayer}";
        }

        private void pictureBoxGame_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
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
                        // Рисуем саму фишку (заливка + рамка + цифра)
                        using (Brush brush = new SolidBrush(_game.TileColor))
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            g.FillRectangle(brush, rect);
                            g.DrawRectangle(pen, rect);
                        }
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
                    int timeSpent = _gameDuration - _timeLeft;

                    SaveRecord(timeSpent);

                    MessageBox.Show(
                        $"Поздравляем, {_currentPlayer}! Вы собрали головоломку!\n" +
                        $"Затрачено времени: {timeSpent} сек.\n" +
                        $"Результат сохранен.", "Победа!");
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

        private void цветФишекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = _game.TileColor;
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    _game.TileColor = cd.Color;
                    pictureBoxGame.Invalidate();
                }
            }
        }

        private void трудноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gameDuration = 180;
            MessageBox.Show("Установлен сложный уровень (3 минуты). Начните новую игру!");
        }

        private void среднийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gameDuration = 300;
            MessageBox.Show("Установлен нормальный уровень (5 минут). Начните новую игру!");
        }

        private void легкий10МинутToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _gameDuration = 600;
            MessageBox.Show("Установлен легкий уровень (10 минут). Начните новую игру!");
        }

        private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
            "Цель игры — расставить костяшки по порядку от 1 до 15, используя пустую клетку.\n\n" +
            "Кликайте мышкой по фишке рядом с пустой клеткой, чтобы передвинуть её.\n" +
            "Успейте собрать головоломку до истечения времени!",
            "Правила игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveRecord(int timeSpent)
        {
            List<GameRecord> records = LoadRecords(); // Загружаем старые рекорды
            records.Add(new GameRecord { PlayerName = _currentPlayer, TimeSpent = timeSpent, Date = DateTime.Now });

            using (FileStream fs = new FileStream(_recordsFile, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, records); // Сохраняем обновленный список
            }
        }

        private List<GameRecord> LoadRecords()
        {
            if (!File.Exists(_recordsFile)) return new List<GameRecord>();

            try
            {
                using (FileStream fs = new FileStream(_recordsFile, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    return (List<GameRecord>)bf.Deserialize(fs); // Читаем из файла
                }
            }
            catch
            {
                return new List<GameRecord>(); // Если файл поврежден, возвращаем пустой список
            }
        }


        private void таблицаРекордовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var records = LoadRecords();
            string result = $"Результаты игрока {_currentPlayer}:\n\n";
            bool found = false;

            foreach (var rec in records)
            {
                if (rec.PlayerName == _currentPlayer)
                {
                    result += $"- {rec.Date.ToShortDateString()}: {rec.TimeSpent} сек.\n";
                    found = true;
                }
            }

            if (!found) result += "Нет сыгранных игр.";
            MessageBox.Show(result, "Статистика", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void авторизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Авторизация",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Width = 240, Text = "Введите ваше имя:" };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
            Button confirmation = new Button() { Text = "ОК", Left = 160, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            prompt.Controls.Add(textLabel); prompt.Controls.Add(textBox); prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            if (prompt.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(textBox.Text))
            {
                _currentPlayer = textBox.Text;
                lblPlayer.Text = $"Игрок: {_currentPlayer}";
                MessageBox.Show($"Добро пожаловать, {_currentPlayer}!");
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
