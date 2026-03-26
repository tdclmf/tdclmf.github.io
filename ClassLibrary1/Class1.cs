using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class Slovar
    {
        private string filename;
        private int count;
        private List<string> words = new List<string>();
        public Slovar(string filename)
        {
            this.filename = filename;
            using (StreamReader sr = new StreamReader(filename))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    words.Add(line);
                }
                words.Count();
            }
        }

        public int Count
        {
            get { return count; }
        }

    }
}
