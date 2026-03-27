using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Slovar
    {
        private string filename;
        private int count;
        private List<string> words = new List<string>();
        public Slovar(string filename)
        {
            this.filename = filename;
            if (File.Exists(filename))
            {
                words = File.ReadAllLines(filename).ToList();
            }
            count = words.Count;
        }

        public bool AddWord(string word)
        {
            string cleanWord = word.Trim();
            if (string.IsNullOrEmpty(cleanWord)) return false;
            if (cleanWord.Contains(" ")) return false;
            foreach (char c in cleanWord)
            {
                if (!char.IsLetter(c) && c != '-') return false;
            }
            if (words.Any(w => w.Equals(cleanWord, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            words.Add(cleanWord);
            count = words.Count;
            return true;
        }

        private int GetLevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s)) return t.Length;
            if (string.IsNullOrEmpty(t)) return s.Length;
            if (Math.Abs(s.Length - t.Length) > 3) return 10;

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; j++) d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        public int FindFuzzyIndex(string query)
        {
            if (string.IsNullOrEmpty(query)) return -1;

            string q = query.ToLower();
            int bestIndex = -1;
            int minDistance = 4;

            for (int i = 0; i < words.Count; i++)
            {
                string currentWord = words[i].ToLower();
                if (currentWord == q) return i;
                int distance = GetLevenshteinDistance(q, currentWord);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    bestIndex = i;
                }
            }

            return bestIndex;
        }

        public void DeleteWord(string word)
        {
            this.words.Remove(word);
            count -= 1;
        }
        public int Count
        {
            get { return count; }
        }
        public void SaveToFile()
        {
            if (!string.IsNullOrEmpty(filename))
            {
                File.WriteAllLines(filename, words);
            }
        }

        public List<string> GetWords(HashSet<char> letters, int length)
        {
            List<string> result = new List<string>();
            bool forbiddenchar;
            foreach (string word in words)
            {
                string lowerword = word.ToLower();
                forbiddenchar = false;
                if (word.Length != length) continue;
                foreach (char c in letters)
                {
                    if (lowerword.Contains(c))
                    {
                        forbiddenchar = true;
                        break;
                    }
                }
                if (!forbiddenchar) result.Add(word); 
            }

            return result;
        }
        public List<string> SearchWords(string query)
        {
            if (string.IsNullOrEmpty(query)) return words;
            return words.Where(w => w.StartsWith(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<string> GetAllWords()
        {
            return words;
        }
        public void SaveResults(string path, List<string> foundWords)
        {
            File.WriteAllLines(path, foundWords);
        }

    }
}
