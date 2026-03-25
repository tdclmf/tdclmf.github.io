using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Task_4
{
    public class Candidate
    {
        public string Name { get; set; }
        public int Votes { get; set; }

        public Candidate(string name, int votes)
        {
            Name = name;
            Votes = votes;
        }
    }

    public class Election
    {
        private List<Candidate> candidates = new List<Candidate>();
        private string defaultTitle = "Результаты голосования";

        public void AddOrUpdate(string name, int votes)
        {
            var existing = candidates.Find(c => c.Name == name);
            if (existing != null)
                existing.Votes += votes;
            else
                candidates.Add(new Candidate(name, votes));
        }

        public List<Candidate> GetAll() => candidates;

        public void SaveToFile(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine(defaultTitle);
                sw.WriteLine(candidates.Count);
                foreach (var c in candidates)
                    sw.WriteLine($"{c.Name};{c.Votes}");
            }
        }

        public void LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName)) return;
            candidates.Clear();
            using (StreamReader sr = new StreamReader(fileName))
            {
                sr.ReadLine();
                if (int.TryParse(sr.ReadLine(), out int count))
                {
                    for (int i = 0; i < count; i++)
                    {
                        string line = sr.ReadLine();
                        if (string.IsNullOrEmpty(line)) continue;
                        string[] parts = line.Split(';');
                        candidates.Add(new Candidate(parts[0], int.Parse(parts[1])));
                    }
                }
            }
        }

        public void DrawDiagram(Chart chart)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            chart.Titles.Add("Доля голосов за кандидатов");
            chart.Titles[0].Font = new Font("Arial", 16, FontStyle.Bold);
            Series series = new Series("VotesSeries");
            series.ChartType = SeriesChartType.Pie;
            chart.Series.Add(series);
            foreach (var c in candidates)
            {
                int i = series.Points.AddXY(c.Name, c.Votes);
                series.Points[i].LegendText = c.Name;
                series.Points[i].Label = "#PERCENT"; 
            }
        }
    }
}
