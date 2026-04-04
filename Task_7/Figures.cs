using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Task_7
{
    [Serializable]
    public class Stroke
    {
        public Color Color { get; set; } = Color.Black;
        public float Width { get; set; } = 1f;
        public DashStyle DashStyle { get; set; } = DashStyle.Solid;

        public Pen GetPen()
        {
            return new Pen(Color, Width) { DashStyle = this.DashStyle };
        }
    }

    [Serializable]
    public abstract class Figure
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Stroke Stroke { get; set; } = new Stroke();

        [NonSerialized]
        public bool IsSelected = false;

        public abstract void Draw(Graphics g);

        public virtual bool Contains(Point p)
        {
            return p.X >= X && p.X <= X + Width && p.Y >= Y && p.Y <= Y + Height;
        }

        // Метод перемещения
        public void Move(float dx, float dy)
        {
            X += dx;
            Y += dy;
        }
        public void DrawMarkers(Graphics g)
        {
            if (!IsSelected) return;
            int s = 6;
            using (Brush b = new SolidBrush(Color.White))
            using (Pen p = new Pen(Color.Black))
            {
                // Углы и середины сторон
                float[] xs = { X, X + Width / 2, X + Width };
                float[] ys = { Y, Y + Height / 2, Y + Height };
                foreach (var mx in xs)
                    foreach (var my in ys)
                    {
                        if (mx == X + Width / 2 && my == Y + Height / 2) continue;
                        g.FillRectangle(b, mx - s / 2, my - s / 2, s, s);
                        g.DrawRectangle(p, mx - s / 2, my - s / 2, s, s);
                    }
            }
        }
        public int GetMarkerIndex(Point p)
        {
            float s = 8;
            float[] xs = { X, X + Width / 2, X + Width };
            float[] ys = { Y, Y + Height / 2, Y + Height };

            int index = 0;
            for (int i = 0; i < 3; i++) // Ряды (Y)
            {
                for (int j = 0; j < 3; j++) // Колонки (X)
                {
                    if (i == 1 && j == 1) continue; // Пропускаем центр фигуры

                    RectangleF markerRect = new RectangleF(xs[j] - s / 2, ys[i] - s / 2, s, s);
                    if (markerRect.Contains(p)) return index;
                    index++;
                }
            }
            return -1; // Не попали в маркер
        }
    }
    [Serializable]
    public class RegularPolygon : Figure
    {
        public int Sides { get; set; } = 5;

        public override void Draw(Graphics g)
        {
            if (Width < 2 || Height < 2) return;
            PointF[] pts = CalculatePoints(Sides, 1f);
            using (Pen pen = Stroke.GetPen()) g.DrawPolygon(pen, pts);
        }

        protected PointF[] CalculatePoints(int n, float innerRatio)
        {
            PointF[] pts = new PointF[n];
            float rx = Width / 2;
            float ry = Height / 2;
            float cx = X + rx;
            float cy = Y + ry;
            for (int i = 0; i < n; i++)
            {
                float angle = (float)(i * 2 * Math.PI / n - Math.PI / 2);
                pts[i] = new PointF(cx + rx * (float)Math.Cos(angle), cy + ry * (float)Math.Sin(angle));
            }
            return pts;
        }
    }

    [Serializable]
    public class Star : RegularPolygon
    {
        public override void Draw(Graphics g)
        {
            if (Width < 2 || Height < 2) return;
            int n = Sides * 2;
            PointF[] pts = new PointF[n];
            float rx = Width / 2; float ry = Height / 2;
            float cx = X + rx; float cy = Y + ry;
            for (int i = 0; i < n; i++)
            {
                float r = (i % 2 == 0) ? 1f : 0.4f; // 0.4 - глубина лучей
                float angle = (float)(i * 2 * Math.PI / n - Math.PI / 2);
                pts[i] = new PointF(cx + rx * r * (float)Math.Cos(angle), cy + ry * r * (float)Math.Sin(angle));
            }
            using (Pen pen = Stroke.GetPen()) g.DrawPolygon(pen, pts);
        }
    }
    [Serializable]
    public class StackMemory
    {
        private readonly int _stackDepth;
        private readonly List<byte[]> _list = new List<byte[]>();

        public StackMemory(int depth) { _stackDepth = depth; }
        public int Count => _list.Count;

        public void Push(List<Figure> figures)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, figures);
                if (_list.Count >= _stackDepth) _list.RemoveAt(0);
                _list.Add(ms.ToArray());
            }
        }

        public List<Figure> Pop()
        {
            if (_list.Count == 0) return null;
            byte[] data = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<Figure>)bf.Deserialize(ms);
            }
        }

        public void Clear() => _list.Clear();
    }

}
