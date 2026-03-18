using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task_2;
using System;
using System.Collections.Generic;

namespace UnitTestsProject
{
    [TestClass]
    public class GeometryHelperTests
    {
        [TestMethod]
        public void TestMethod1_SingleClosestPair()
        {
            Point2D[] points = { new Point2D(1, 1), new Point2D(2, 2), new Point2D(-5, -5) };
            int[] quarters = { 1 };

            GeometryHelper.FindClosestPairsInQuarters(points, quarters, out double minDistance, out List<PointPair> pairs);
            // Ожидаемое расстояние: sqrt((2-1)^2 + (2-1)^2) = sqrt(2) ≈ 1.414213
            Assert.AreEqual(Math.Sqrt(2), minDistance, 0.0001, "Неверное минимальное расстояние.");
            Assert.AreEqual(1, pairs.Count, "Ожидалась ровно одна пара.");
        }

        [TestMethod]
        public void TestMethod2_MultipleClosestPairs()
        {
            // Две пары точек находятся на расстоянии 1 друг от друга
            Point2D[] points = { new Point2D(1, 1), new Point2D(2, 1), new Point2D(1, 3), new Point2D(2, 3) };
            int[] quarters = { 1 };

            GeometryHelper.FindClosestPairsInQuarters(points, quarters, out double minDistance, out List<PointPair> pairs);

            Assert.AreEqual(1.0, minDistance, 0.0001);
            Assert.AreEqual(2, pairs.Count, "Ожидалось две пары с одинаковым минимальным расстоянием.");
        }

        [TestMethod]
        public void TestMethod3_PointsOnAxes_Ignored()
        {
            Point2D[] points = { new Point2D(1, 1), new Point2D(2, 2), new Point2D(0, 5), new Point2D(5, 0) };
            int[] quarters = { 1 };

            GeometryHelper.FindClosestPairsInQuarters(points, quarters, out double minDistance, out List<PointPair> pairs);

            Assert.AreEqual(Math.Sqrt(2), minDistance, 0.0001);
            Assert.AreEqual(1, pairs.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMethod4_NullArray_ThrowsException()
        {
            int[] quarters = { 1 };
            GeometryHelper.FindClosestPairsInQuarters(null, quarters, out _, out _);
        }

        [TestMethod]
        public void TestMethod5_NotEnoughPointsInQuarter_ThrowsException()
        {
            Point2D[] points = { new Point2D(1, 1), new Point2D(-2, 2), new Point2D(-3, -3) };
            int[] quarters = { 1 }; // В первой четверти только одна точка
            try
            {
                GeometryHelper.FindClosestPairsInQuarters(points, quarters, out _, out _);
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, "менее двух точек");
                return;
            }
            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }

        [TestMethod]
        public void TestMethod6_NoPointsInQuarter_ThrowsException()
        {
            Point2D[] points = { new Point2D(-1, 1), new Point2D(-2, 2) };
            int[] quarters = { 1, 4 }; // В 1 и 4 четвертях точек нет

            try
            {
                GeometryHelper.FindClosestPairsInQuarters(points, quarters, out _, out _);
            }
            catch (ArgumentException ex)
            {
                StringAssert.Contains(ex.Message, "менее двух точек");
                return;
            }
            Assert.Fail("Ожидаемое исключение не было выброшено.");
        }
    }
}