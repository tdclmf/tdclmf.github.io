using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_2
{
    public struct Point2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}; {Y})";
    }

    public struct PointPair
    {
        public Point2D P1 { get; set; }
        public Point2D P2 { get; set; }

        public PointPair(Point2D p1, Point2D p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public override string ToString() => $"{P1} и {P2}";
    }

    public class GeometryHelper
    {
        public static double GetDistance(Point2D p1, Point2D p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        public static int GetQuarter(Point2D p)
        {
            if (p.X > 0 && p.Y > 0) return 1;
            if (p.X < 0 && p.Y > 0) return 2;
            if (p.X < 0 && p.Y < 0) return 3;
            if (p.X > 0 && p.Y < 0) return 4;
            return 0;
        }

        public static void FindClosestPairsInQuarters(Point2D[] points, int[] targetQuarters, out double minDistance, out List<PointPair> closestPairs)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points), "Массив точек не может быть пустым.");

            var validPoints = points.Where(p => targetQuarters.Contains(GetQuarter(p))).ToArray();

       
            if (validPoints.Length < 2)
            {
                throw new ArgumentException("В заданных координатных четвертях находится менее двух точек. Функция не определена.");
            }

            minDistance = double.MaxValue;
            closestPairs = new List<PointPair>();
            const double epsilon = 1e-7;
            for (int i = 0; i < validPoints.Length - 1; i++)
            {
                for (int j = i + 1; j < validPoints.Length; j++)
                {
                    double dist = GetDistance(validPoints[i], validPoints[j]);
                    if (dist < minDistance - epsilon)
                    {
                        minDistance = dist;
                        closestPairs.Clear();
                        closestPairs.Add(new PointPair(validPoints[i], validPoints[j]));
                    }
                    else if (Math.Abs(dist - minDistance) < epsilon)
                    {
                        closestPairs.Add(new PointPair(validPoints[i], validPoints[j]));
                    }
                }
            }
        }
    }
}