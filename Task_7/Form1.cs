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

namespace Task_7
{
    public partial class Form1 : Form
    {
        private byte[] _clipboardBuffer = null;
        private int _currentMarker = -1;
        private StackMemory _undoStack = new StackMemory(20);
        private StackMemory _redoStack = new StackMemory(20);
        private List<Figure> _figures = new List<Figure>(); // Список всех фигур
        private Figure _selectedFigure = null;              // Текущая выделенная фигура
        private Figure _tempFigure = null;                  // Фигура, которую мы тянем мышкой прямо сейчас
        private string _currentTool = "Pointer";            // Текущий инструмент
        private StackMemory _history = new StackMemory(20); // Память для Undo
        private Point _startPoint;                          // Точка начала рисования
        private Stroke _currentStroke = new Stroke();       // Текущие настройки пера
        public Form1()
        {
            InitializeComponent();
            CreateColorPalette();
            cmbThickness.Items.Clear();
            cmbThickness.Items.AddRange(new object[] { "1", "2", "3", "5", "8", "12" });
            cmbThickness.SelectedIndex = 0;
        }
        private void CreateColorPalette()
        {
            // Список цветов как на скрине (можно добавить свои)
            Color[] colors = {
            Color.Black, Color.Gray, Color.DarkRed, Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple, Color.White,
            Color.LightGray, Color.Brown, Color.Pink, Color.Gold, Color.LightYellow, Color.LightGreen, Color.LightBlue, Color.Violet
            };

            foreach (Color c in colors)
            {
                // Создаем маленькую "кнопку" для каждого цвета
                Label colorBox = new Label();
                colorBox.Size = new Size(18, 18);
                colorBox.BackColor = c;
                colorBox.BorderStyle = BorderStyle.FixedSingle;
                colorBox.Margin = new Padding(1); // Расстояние между квадратиками

                // Привязываем событие клика
                colorBox.Click += ColorBox_Click;

                // Добавляем в нашу панель
                colorPalette.Controls.Add(colorBox);
            }
        }
        private void ColorBox_Click(object sender, EventArgs e)
        {
            Label clickedBox = (Label)sender;
            _currentStroke.Color = clickedBox.BackColor;
            currColorIndicator.BackColor = clickedBox.BackColor;
            if (_selectedFigure != null)
            {
                _selectedFigure.Stroke.Color = clickedBox.BackColor;
                Invalidate();
            }
        }

        private void cmbThickness_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbThickness.SelectedItem == null) return;

            float w = float.Parse(cmbThickness.SelectedItem.ToString());
            _currentStroke.Width = w;

            if (_selectedFigure != null)
            {
                _selectedFigure.Stroke.Width = w;
                Invalidate();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Рисуем сетку
            using (Pen gridPen = new Pen(Color.FromArgb(240, 240, 240))) // Очень светлый серый
            {
                for (int i = 0; i < this.Width; i += 20)
                    g.DrawLine(gridPen, i, 0, i, this.Height);
                for (int i = 0; i < this.Height; i += 20)
                    g.DrawLine(gridPen, 0, i, this.Width, i);
            }

            // Рисование фигур...
            foreach (var fig in _figures) { fig.Draw(g); fig.DrawMarkers(g); }
        }
        private void btnPointer_Click(object sender, EventArgs e) => _currentTool = "Pointer";
        private void btnPolygon_Click(object sender, EventArgs e) => _currentTool = "Polygon";
        private void btnStar_Click(object sender, EventArgs e) => _currentTool = "Star";
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            SaveForUndo();
            _startPoint = e.Location;
            _currentMarker = -1;

            if (_currentTool == "Pointer")
            {
                if (_selectedFigure != null)
                {
                    _currentMarker = _selectedFigure.GetMarkerIndex(e.Location);
                }
                if (_currentMarker == -1)
                {
                    _selectedFigure = null;
                    foreach (var fig in _figures) fig.IsSelected = false;

                    for (int i = _figures.Count - 1; i >= 0; i--)
                    {
                        if (_figures[i].Contains(e.Location))
                        {
                            _selectedFigure = _figures[i];
                            _selectedFigure.IsSelected = true;
                            _currentStroke.Color = _selectedFigure.Stroke.Color;
                            currColorIndicator.BackColor = _selectedFigure.Stroke.Color;
                            break;
                        }
                    }
                }
            }
            else
            {
                _history.Push(_figures.Select(f => (Figure)f).ToList());

                if (_currentTool == "Polygon") _tempFigure = new RegularPolygon();
                else if (_currentTool == "Star") _tempFigure = new Star();

                if (_tempFigure != null)
                {
                    _tempFigure.X = e.X;
                    _tempFigure.Y = e.Y;
                    _tempFigure.Width = 0;
                    _tempFigure.Height = 0;
                    _tempFigure.Stroke.Color = _currentStroke.Color;
                    _tempFigure.Stroke.Width = _currentStroke.Width;
                    _figures.Add(_tempFigure);
                }
            }
            Invalidate(); // Перерисовать всё
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_tempFigure != null) // Рисуем новую
                {
                    _tempFigure.Width = Math.Abs(e.X - _startPoint.X);
                    _tempFigure.Height = Math.Abs(e.Y - _startPoint.Y);
                    _tempFigure.X = Math.Min(e.X, _startPoint.X);
                    _tempFigure.Y = Math.Min(e.Y, _startPoint.Y);
                }
                else if (_selectedFigure != null && _currentMarker != -1) // РАСТЯГИВАЕМ
                {
                    float dx = e.X - _startPoint.X;
                    float dy = e.Y - _startPoint.Y;

                    switch (_currentMarker)
                    {
                        case 0: // Левый верхний
                            _selectedFigure.X += dx; _selectedFigure.Y += dy;
                            _selectedFigure.Width -= dx; _selectedFigure.Height -= dy;
                            break;
                        case 1: // Средний верхний
                            _selectedFigure.Y += dy; _selectedFigure.Height -= dy;
                            break;
                        case 2: // Правый верхний
                            _selectedFigure.Y += dy; _selectedFigure.Width += dx; _selectedFigure.Height -= dy;
                            break;
                        case 3: // Средний левый
                            _selectedFigure.X += dx; _selectedFigure.Width -= dx;
                            break;
                        case 4: // Средний правый
                            _selectedFigure.Width += dx;
                            break;
                        case 5: // Левый нижний
                            _selectedFigure.X += dx; _selectedFigure.Width -= dx; _selectedFigure.Height += dy;
                            break;
                        case 6: // Средний нижний
                            _selectedFigure.Height += dy;
                            break;
                        case 7: // Правый нижний
                            _selectedFigure.Width += dx; _selectedFigure.Height += dy;
                            break;
                    }
                    _startPoint = e.Location;
                }
                else if (_selectedFigure != null) // Просто двигаем
                {
                    float dx = e.X - _startPoint.X;
                    float dy = e.Y - _startPoint.Y;
                    _selectedFigure.Move(dx, dy);
                    _startPoint = e.Location;
                }
                Invalidate();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _tempFigure = null; // Заканчиваем создание
            Invalidate();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "Векторный рисунок|*.vec" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, _figures);
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedFigure != null)
            {
                _history.Push(_figures.ToList());
                _figures.Remove(_selectedFigure);
                _selectedFigure = null;
                Invalidate();
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_selectedFigure == null) return;
            int step = e.Shift ? 1 : 5;
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelected();
                e.Handled = true;
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteSelected();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Up) _selectedFigure.Move(0, -step);
            if (e.KeyCode == Keys.Down) _selectedFigure.Move(0, step);
            if (e.KeyCode == Keys.Left) _selectedFigure.Move(-step, 0);
            if (e.KeyCode == Keys.Right) _selectedFigure.Move(step, 0);
            if (e.KeyCode == Keys.Delete) btnDelete_Click(null, null);

            Invalidate();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog { Filter = "Векторный рисунок|*.vec" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(ofd.FileName, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    _figures = (List<Figure>)bf.Deserialize(fs);
                    _selectedFigure = null;
                    Invalidate();
                }
            }
        }
        private void CopySelected()
        {
            if (_selectedFigure == null) return;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                // Сериализуем (превращаем в байты) список из одной фигуры
                bf.Serialize(ms, new List<Figure> { _selectedFigure });
                _clipboardBuffer = ms.ToArray(); // Сохраняем байты в буфер
            }
        }

        private void PasteSelected()
        {
            if (_clipboardBuffer == null) return;

            // Сохраняем текущее состояние в историю для Undo
            _history.Push(_figures.ToList());

            using (MemoryStream ms = new MemoryStream(_clipboardBuffer))
            {
                BinaryFormatter bf = new BinaryFormatter();
                // Десериализуем (создаем новую фигуру из байтов)
                List<Figure> pastedList = (List<Figure>)bf.Deserialize(ms);
                Figure newFig = pastedList[0];

                // Сдвигаем, чтобы не было наложения
                newFig.X += 15;
                newFig.Y += 15;

                // Выделяем новую фигуру
                if (_selectedFigure != null) _selectedFigure.IsSelected = false;
                _selectedFigure = newFig;
                _selectedFigure.IsSelected = true;

                _figures.Add(newFig);
            }
            Invalidate();
        }
        private void SaveForUndo()
        {
            _undoStack.Push(_figures.ToList());
            _redoStack.Clear(); // Любое новое действие обнуляет путь "вперед" (Redo)
        }

        private void Undo()
        {
            if (_undoStack.Count > 0)
            {
                // Прежде чем откатиться, сохраняем текущее состояние в Redo
                _redoStack.Push(_figures.ToList());

                // Достаем из Undo
                _figures = _undoStack.Pop();
                _selectedFigure = null;
                Invalidate();
            }
        }

        private void Redo()
        {
            if (_redoStack.Count > 0)
            {
                // Перед возвратом вперед, сохраняем текущее в Undo
                _undoStack.Push(_figures.ToList());

                // Достаем из Redo
                _figures = _redoStack.Pop();
                _selectedFigure = null;
                Invalidate();
            }
        }

        private void Undo(object sender, EventArgs e)
        {
            Undo();
        }

        private void Redo(object sender, EventArgs e)
        {
            Redo();
        }
    }
}
