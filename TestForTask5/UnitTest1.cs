using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ClassLibrary1;
using System.Collections.Generic;
using System.IO;

namespace TestForTask5
{
    [TestClass]
    public class SlovarUnitTests
    {
        private string tempFilePath;

        [TestInitialize]
        public void Setup()
        {
            tempFilePath = Path.GetTempFileName();
            File.WriteAllLines(tempFilePath, new string[] { "яблоко", "корова", "банан", "рубль" });
        }

        [TestCleanup]
        public void TearDown()
        {
            if (File.Exists(tempFilePath))
                File.Delete(tempFilePath);
        }

        [TestMethod]
        public void AddWord_ShouldNotAddDuplicate()
        {
            var slovar = new Slovar(tempFilePath);
            int initialCount = slovar.GetAllWords().Count;
            slovar.AddWord("ЯБЛОКО");
            Assert.AreEqual(initialCount, slovar.GetAllWords().Count, "Дубликат слова был добавлен!");
        }

        [TestMethod]
        public void GetWords_FilterByLengthAndForbiddenLetters()
        {
            var slovar = new Slovar(tempFilePath);
            HashSet<char> forbidden = new HashSet<char> { 'б', 'н' };
            int length = 6;
            var result = slovar.GetWords(forbidden, length);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("корова", result[0]);
        }

        [TestMethod]
        public void FindFuzzyIndex_ShouldFindClosestWord_RublToRubl()
        {
            var slovar = new Slovar(tempFilePath);
            int index = slovar.FindFuzzyIndex("рубл");
            Assert.AreNotEqual(-1, index);
            Assert.AreEqual("рубль", slovar.GetAllWords()[index]);
        }

        [TestMethod]
        public void DeleteWord_ShouldDecreaseCount()
        {
            var slovar = new Slovar(tempFilePath);
            int initialCount = slovar.Count;
            slovar.DeleteWord("банан");
            Assert.AreEqual(initialCount - 1, slovar.Count);
            Assert.IsFalse(slovar.GetAllWords().Contains("банан"));
        }

        [TestMethod]
        public void SearchWords_ShouldReturnPrefixMatches()
        {
            var slovar = new Slovar(tempFilePath);
            var result = slovar.SearchWords("ябл");
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("яблоко", result[0]);
        }
    }
}
